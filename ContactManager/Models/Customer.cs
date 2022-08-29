
namespace ContactManager.Models
{
    public class Customer : Contact
    {
		private string? _notes;

		public string? Notes
		{
			get { return _notes; }
			set { _notes = value; }
		}

	}
}
