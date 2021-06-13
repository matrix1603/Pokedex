using System.Collections.Generic;
using System.Linq;

namespace PokeDex.Services
{
	public class ServiceResult<T>
	{
		public ServiceResult()
		{
			Errors = new List<string>();
		}
		public T Entity { get; set; }
		public List<string> Errors { get; set; }

		public string ErrorMessage => string.Join(",", Errors);

		public bool HasError => Errors.Any();

	}
}
