namespace WebWidget.DataAccess
{
    using System.Data.Entity;
    using WebWidget.DTO.Models;

    public class WidgetDbInitializer : DropCreateDatabaseAlways<WidgetContext>
    {
        protected override void Seed(WidgetContext db)
        {
            db.Widgets.Add(new Widget
            {
                Id = 1,
                Name = "Widget 1",
                Description = "Widget for testing.",
                Price = 4.5
            });
            db.Widgets.Add(new Widget
            {
                Id = 2,
                Name = "Widget 2",
                Description = "Widget for testing.",
                Price = 7.59
            });
            db.Widgets.Add(new Widget
            {
                Id = 3,
                Name = "Widget 3",
                Description = "Widget for testing.",
                Price = 12.99
            });

            base.Seed(db);
        }
    }
}