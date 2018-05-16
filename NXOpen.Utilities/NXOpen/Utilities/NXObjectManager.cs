namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class NXObjectManager : NXRemotableObject
    {
        private static Dictionary<string, Type> classMap = new Dictionary<string, Type>();
        private static Dictionary<Tag, string> classNameMap = new Dictionary<Tag, string>();
        private static Dictionary<Tag, DeletedObjectQueue> deletedObjectMap = new Dictionary<Tag, DeletedObjectQueue>();
        private static int handlerId = -1;
        private static TagEventHandler myHandler;
        private static Dictionary<Tag, ObjectMapEntry> objectMap = new Dictionary<Tag, ObjectMapEntry>();
        private static EventHandler unloadHandler = new EventHandler(NXObjectManager.UnloadHandler);

        static NXObjectManager()
        {
            AppDomain.CurrentDomain.DomainUnload += unloadHandler;
        }

        public NXObjectManager()
        {
            RegisterTagEventHandler();
        }

        public static TransientObject Construct(string className, IntPtr arg)
        {
            Type type;
            RegisterTagEventHandler();
            classMap.TryGetValue(className, out type);
            if (type == null)
            {
                type = FindTypeForClass(className, typeof(TransientObject));
            }
            if (type == null)
            {
                throw new Exception("No automation class found: " + className);
            }
            Type[] types = new Type[] { typeof(IntPtr) };
            ConstructorInfo info = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            if (info == null)
            {
                throw new Exception("No constructor found for: " + type);
            }
            return (TransientObject) info.Invoke(new object[] { arg });
        }

        private static void Delete(Tag tag)
        {
            ObjectMapEntry entry;
            objectMap.TryGetValue(tag, out entry);
            if (entry != null)
            {
                DeletedObjectQueue queue;
                objectMap.Remove(tag);
                classNameMap.Remove(tag);
                deletedObjectMap.TryGetValue(tag, out queue);
                if (queue == null)
                {
                    queue = new DeletedObjectQueue();
                    deletedObjectMap[tag] = queue;
                }
                queue.Add(entry);
                entry.Obj.SetTag(Tag.Null);
            }
            else
            {
                DeletedObjectQueue queue2;
                deletedObjectMap.TryGetValue(tag, out queue2);
                if (queue2 != null)
                {
                    ObjectMapEntry newest = queue2.Newest;
                    newest.RemainingUndoOverDelete++;
                }
            }
        }

        private static Type FindTypeForClass(string className, Type typeToMatch)
        {
            Type type = Type.GetType("NXOpen." + className + ", NXOpen");
            if (type != null)
            {
                classMap[className] = type;
                return type;
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                type = assembly.GetType("NXOpen." + className);
                if (type != null)
                {
                    classMap[className] = type;
                    return type;
                }
            }
            foreach (Assembly assembly2 in assemblies)
            {
                if (!assembly2.GlobalAssemblyCache && !assembly2.GetName().Name.Equals("mscorlib"))
                {
                    Type[] types = assembly2.GetTypes();
                    for (int i = 0; i < types.Length; i++)
                    {
                        if ((types[i].FullName.StartsWith("NXOpen") && (types[i].Name == className)) && types[i].IsSubclassOf(typeToMatch))
                        {
                            classMap[className] = types[i];
                            return types[i];
                        }
                    }
                }
            }
            return null;
        }

        public static TaggedObject Get(Tag objectTag)
        {
            return Get(objectTag, null);
        }

        public static TaggedObject Get(Tag objectTag, string name)
        {
            ObjectMapEntry entry;
            string str;
            string str2;
            Type type;
            RegisterTagEventHandler();
            if (objectTag == Tag.Null)
            {
                return null;
            }
            objectMap.TryGetValue(objectTag, out entry);
            classNameMap.TryGetValue(objectTag, out str);
            if (name != null)
            {
                str2 = name;
            }
            else
            {
                int error = 0;
                IntPtr s = JAM_ask_object_class_name_managed(objectTag, ref error);
                if (error != 0)
                {
                    throw new Exception(string.Concat(new object[] { "Could not get class name for tag: ", objectTag, ", error: ", error }));
                }
                str2 = JAM.ToStringFromLocale(s);
            }
            if ((entry != null) && (str2 != str))
            {
                Delete(objectTag);
                entry = null;
            }
            if (entry != null)
            {
                return entry.Obj;
            }
            classMap.TryGetValue(str2, out type);
            if (type == null)
            {
                type = FindTypeForClass(str2, typeof(TaggedObject));
            }
            if (type == null)
            {
                throw new Exception(string.Concat(new object[] { "No automation class for tag: ", objectTag, ", class: ", str2 }));
            }
            ConstructorInfo info = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            if (info == null)
            {
                throw new Exception("No constructor found for: " + type);
            }
            TaggedObject obj2 = (TaggedObject) info.Invoke(null);
            MethodInfo info2 = type.GetMethod("initialize", BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            if (info2 != null)
            {
                info2.Invoke(obj2, null);
            }
            ObjectMapEntry entry2 = new ObjectMapEntry(obj2, str2);
            objectMap[objectTag] = entry2;
            classNameMap[objectTag] = str2;
            obj2.SetTag(objectTag);
            return obj2;
        }

        public static TaggedObject GetObjectFromUInt(uint tagValue)
        {
            return Get((Tag) tagValue);
        }

        public TaggedObject GetTaggedObject(Tag objectTag)
        {
            return Get(objectTag, null);
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr JAM_ask_object_class_name_managed(Tag tag, ref int error);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_register_managed_tag_event_handler(TagEventMaskType mask, TagEventHandler handler, ref int id);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_unregister_tag_event_handler(int id);
        private static void RegisterTagEventHandler()
        {
            if (myHandler == null)
            {
                myHandler = new TagEventHandler(NXObjectManager.TagHandler);
                TagEventMaskType mask = TagEventMaskType.TAG_EVENT_UNDO_DELETE_DISCARDED_MASK | TagEventMaskType.TAG_EVENT_UNDO_DELETE_EXPIRED_MASK | TagEventMaskType.TAG_EVENT_UNDO_OVER_DELETE_MASK | TagEventMaskType.TAG_EVENT_NORMAL_DELETE_MASK | TagEventMaskType.TAG_EVENT_UNDO_OVER_CREATE_MASK;
                JAM_register_managed_tag_event_handler(mask, myHandler, ref handlerId);
            }
        }

        private static void TagHandler(TagEventType eventType, Tag tag)
        {
            switch (eventType)
            {
                case TagEventType.TAG_EVENT_UNDO_OVER_CREATE:
                    ObjectMapEntry entry2;
                    objectMap.TryGetValue(tag, out entry2);
                    if (entry2 != null)
                    {
                        objectMap.Remove(tag);
                        entry2.Obj.SetTag(Tag.Null);
                    }
                    break;

                case TagEventType.TAG_EVENT_NORMAL_DELETE:
                    Delete(tag);
                    return;

                case TagEventType.TAG_EVENT_UNDO_OVER_DELETE:
                {
                    ObjectMapEntry entry;
                    DeletedObjectQueue queue;
                    objectMap.TryGetValue(tag, out entry);
                    if (entry != null)
                    {
                        throw new ApplicationException("Unexpected map entry for: " + tag);
                    }
                    deletedObjectMap.TryGetValue(tag, out queue);
                    if (queue == null)
                    {
                        break;
                    }
                    if (queue.Newest.RemainingUndoOverDelete == 0)
                    {
                        objectMap[tag] = queue.Newest;
                        queue.Newest.Obj.SetTag(tag);
                        queue.RemoveNewest();
                        if (queue.Count == 0)
                        {
                            deletedObjectMap.Remove(tag);
                            return;
                        }
                        break;
                    }
                    ObjectMapEntry newest = queue.Newest;
                    newest.RemainingUndoOverDelete--;
                    return;
                }
                case TagEventType.TAG_EVENT_UNDO_DELETE_EXPIRED:
                    DeletedObjectQueue queue2;
                    deletedObjectMap.TryGetValue(tag, out queue2);
                    if (queue2 == null)
                    {
                        break;
                    }
                    queue2.RemoveOldest();
                    if (queue2.Count != 0)
                    {
                        break;
                    }
                    deletedObjectMap.Remove(tag);
                    return;

                case TagEventType.TAG_EVENT_UNDO_CREATE_EXPIRED:
                case TagEventType.TAG_EVENT_BEFORE_REPLACE:
                case TagEventType.TAG_EVENT_AFTER_REPLACE:
                    break;

                case TagEventType.TAG_EVENT_UNDO_DELETE_DISCARDED:
                    DeletedObjectQueue queue3;
                    deletedObjectMap.TryGetValue(tag, out queue3);
                    if (queue3 == null)
                    {
                        break;
                    }
                    if (queue3.Newest.RemainingUndoOverDelete != 0)
                    {
                        ObjectMapEntry entry3 = queue3.Newest;
                        entry3.RemainingUndoOverDelete--;
                        return;
                    }
                    queue3.RemoveNewest();
                    if (queue3.Count != 0)
                    {
                        break;
                    }
                    deletedObjectMap.Remove(tag);
                    return;

                default:
                    return;
            }
        }

        private static void UnloadHandler(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.DomainUnload -= unloadHandler;
            if (handlerId >= 0)
            {
                JAM_unregister_tag_event_handler(handlerId);
            }
            myHandler = null;
            handlerId = -1;
        }

        private delegate void TagEventHandler(NXObjectManager.TagEventType eventType, Tag tag);

        [Flags]
        private enum TagEventMaskType
        {
            TAG_EVENT_AFTER_REPLACE_MASK = 0x80,
            TAG_EVENT_BEFORE_REPLACE_MASK = 0x40,
            TAG_EVENT_NORMAL_CREATE_MASK = 1,
            TAG_EVENT_NORMAL_DELETE_MASK = 4,
            TAG_EVENT_UNDO_CREATE_EXPIRED_MASK = 0x20,
            TAG_EVENT_UNDO_DELETE_DISCARDED_MASK = 0x100,
            TAG_EVENT_UNDO_DELETE_EXPIRED_MASK = 0x10,
            TAG_EVENT_UNDO_OVER_CREATE_MASK = 2,
            TAG_EVENT_UNDO_OVER_DELETE_MASK = 8
        }

        private enum TagEventType
        {
            TAG_EVENT_NORMAL_CREATE,
            TAG_EVENT_UNDO_OVER_CREATE,
            TAG_EVENT_NORMAL_DELETE,
            TAG_EVENT_UNDO_OVER_DELETE,
            TAG_EVENT_UNDO_DELETE_EXPIRED,
            TAG_EVENT_UNDO_CREATE_EXPIRED,
            TAG_EVENT_BEFORE_REPLACE,
            TAG_EVENT_AFTER_REPLACE,
            TAG_EVENT_UNDO_DELETE_DISCARDED
        }
    }
}

