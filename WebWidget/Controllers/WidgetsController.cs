namespace WebWidget.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using WebWidget.DataAccess.Repositories;
    using WebWidget.DTO.Models;

    public class WidgetsController : ApiController
    {
        private readonly IWidgetRepository _widgetRepository;

        public WidgetsController(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        // GET: api/Widgets
        public async Task<IHttpActionResult> GetWidgets()
        {
            List<Widget> widgets = await _widgetRepository.GetAll();
            return Ok(widgets);
        }

        // GET: api/Widgets/5
        [ResponseType(typeof(Widget))]
        public async Task<IHttpActionResult> GetWidget(int id)
        {
            Widget widget = await _widgetRepository.Get(id);
            if (widget == null)
            {
                return NotFound();
            }

            return Ok(widget);
        }

        // PUT: api/Widgets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWidget(int id, Widget widget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != widget.Id)
            {
                return BadRequest();
            }

            bool widgetUpdated = await _widgetRepository.Update(widget);
            if (!widgetUpdated)
            {
                return NotFound();
            }

            return Ok(widget);
        }

        // POST: api/Widgets
        [ResponseType(typeof(Widget))]
        public async Task<IHttpActionResult> PostWidget(Widget widget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Widget createdWidget = await _widgetRepository.Create(widget);
            return CreatedAtRoute("WebWidgetApi", new { id = createdWidget.Id }, createdWidget);
        }

        // DELETE: api/Widgets/5
        [ResponseType(typeof(Widget))]
        public async Task<IHttpActionResult> DeleteWidget(int id)
        {
            var widget = await _widgetRepository.Delete(id);

            if (widget == null)
            {
                return NotFound();
            }

            return Ok(widget);
        }
    }
}