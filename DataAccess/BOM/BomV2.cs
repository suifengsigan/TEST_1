using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class BomV2:IBom
    {
        public void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID, bool isImportEman, List<EACT_CUPRUM_EXP> cuprumEXPs = null)
        {

        }

        public List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames, string modelNo, string partNo)
        {
            return new List<EACT_CUPRUM>();
        }
    }
}
