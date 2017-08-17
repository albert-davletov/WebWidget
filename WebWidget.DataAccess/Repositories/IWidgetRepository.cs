namespace WebWidget.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebWidget.DTO.Models;

    public interface IWidgetRepository
    {
        Task<Widget> Create(Widget widget);

        Task<Widget> Get(int id);

        Task<List<Widget>> GetAll();

        Task<bool> Update(Widget widget);

        Task<Widget> Delete(int id);
    }
}
