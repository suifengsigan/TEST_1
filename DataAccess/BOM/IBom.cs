using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public interface IBom
    {
        void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID, bool isImportEman, string emanWebPath, List<EACT_CUPRUM_EXP> cuprumEXPs = null);
        List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames, string modelNo, string partNo);
        void UpdateCuprumDISCHARGING(List<EACT_CUPRUM> CupRumList);
        void UploadAutoCMMRecord(EACT_AUTOCMM_RECORD record);
        bool IsConnect();
    }
}
