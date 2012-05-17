using System.Media;
using System.Web;
using NHibernate;

namespace NHibernateCourse.Infrastructure
{
	public class DontHurtMe : EmptyInterceptor
	{
		public static int NumberOfRequests
		{
			get
			{
				var o = HttpContext.Current.Items["number-of-requests"];
				if (o != null)
					return (int)o;
				return 0;
			}
			set { HttpContext.Current.Items["number-of-requests"] = value; }
		}

		public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
		{
			var i = NumberOfRequests += 1;
			if (i > 30)
			{
				var screams = new[]
				{
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/ahhh.wav",
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/cri-d-effroi-scream.wav",
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/ciglik3.wav",
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/2scream.wav",
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/scream13.wav",
					"http://www.shockwave-sound.com/sound-effects/scream-sounds/scream4.wav"
				};
				var soundPlayer = new SoundPlayer(screams[i % screams.Length]);
				soundPlayer.PlaySync();
			}
			return base.OnPrepareStatement(sql);
		}
	}
}