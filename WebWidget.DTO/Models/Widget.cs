namespace WebWidget.DTO.Models
{
    /// <summary>
    /// Widget model
    /// </summary>
    public class Widget
    {
        /// <summary>
        /// Unique identificator
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Widget name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Widget description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Widget price
        /// </summary>
        public double Price { get; set; }
    }
}