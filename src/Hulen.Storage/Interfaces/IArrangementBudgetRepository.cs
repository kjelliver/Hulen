using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IArrangementBudgetRepository
    {
        void SaveOne(ArrangementBudgetDTO dto);
        ArrangementBudgetDTO GetOne(int id);
        void UpdateOne(ArrangementBudgetDTO dto);
        void DeleteOne(ArrangementBudgetDTO dto);

        IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpan(DateTime fromDate, DateTime toDate);
        IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpanAndStatus(DateTime fromDate, DateTime toDate, int status);
        IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpanAndStatusSpan(DateTime fromDate, DateTime toDate, int fromStatus, int toStatus);
    }
}
