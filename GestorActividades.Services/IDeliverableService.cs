using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;

namespace GestorActividades.Services
{
    public interface IDeliverableService
    {
        ResponseDto<Deliverable> AddDeliverable(Deliverable Deliverable);

        ResponseDto<Deliverable> GetDeliverableById(int id);

        ResponseDto<Deliverable> UpdateDeliverable(Deliverable Deliverable);

        ResponseDto<bool> DeleteDeliverable(int id);
    }
}