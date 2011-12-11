using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IFixedArrangementCostsRepository
    {
        FixedArrangementCostsDTO GetOne();
        void UpdateOne(FixedArrangementCostsDTO dto);
    }
}
