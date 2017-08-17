namespace WebWidget.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using WebWidget.DTO.Models;

    public class WidgetRepository : IWidgetRepository, IDisposable
    {
        private readonly WidgetContext _context;

        public WidgetRepository(WidgetContext context)
        {
            _context = context;
        }

        public async Task<Widget> Create(Widget widget)
        {
            _context.Widgets.Add(widget);
            await _context.SaveChangesAsync();
            return widget;
        }

        public async Task<Widget> Get(int id)
        {
            return await _context.Widgets.FindAsync(id);
        }

        public async Task<List<Widget>> GetAll()
        {
            return await _context.Widgets.ToListAsync();
        }

        public async Task<bool> Update(Widget widget)
        {
            _context.SetModifiedState(widget);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                var widgetExists = await _context.Widgets.LongCountAsync(e => e.Id == widget.Id);
                if (widgetExists == 0)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Widget> Delete(int id)
        {
            Widget widget = await _context.Widgets.FindAsync(id);
            if (widget == null)
            {
                return null;
            }

            Widget removedWidget = _context.Widgets.Remove(widget);
            await _context.SaveChangesAsync();
            return removedWidget;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}