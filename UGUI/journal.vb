' NX 6.0.5.3
' Journal created by PENGHUI on Mon Apr 23 17:38:26 2018 中国标准时间
'
Option Strict Off
Imports System
Imports NXOpen

Module NXJournal
Sub Main

Dim theSession As Session = Session.GetSession()
Dim workPart As Part = theSession.Parts.Work

Dim displayPart As Part = theSession.Parts.Display

' ----------------------------------------------
'   菜单：刀具->Process Specific->注塑模向导->注塑模工具->Projection Area...
' ----------------------------------------------
' ----------------------------------------------
'   菜单：刀具->操作记录->Stop Recording
' ----------------------------------------------

End Sub
End Module