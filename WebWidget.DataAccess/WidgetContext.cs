namespace WebWidget.DataAccess
{
    using System.Data.Entity;
    using WebWidget.DTO.Models;

    public class WidgetContext : DbContext
    {
        public WidgetContext()
            : base("name=WidgetContext")
        {
        }

        public virtual DbSet<Widget> Widgets { get; set; }

        public virtual void SetModifiedState(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}