using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security
{
	public class Jwtoptions
	{
		public string SecretKey { get; set; }
		public string Issure {  get; set; }
		public string Audience {  get; set; }
		public double DurationInDays {  get; set; }

	}
}
