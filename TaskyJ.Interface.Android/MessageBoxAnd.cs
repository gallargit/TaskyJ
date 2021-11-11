//syncronous alertbox - W.I.P.
using Android.App;
using Android.Content;

namespace TaskyJ.Interface.Android
{
	public static class MessageBoxAnd ///: AlertDialog
	{
		static int result; //1-2-3

		static string Title;
		static string Message;
		static string Button1Text = null;
		static string Button2Text = null;
		static string Button3Text = null;
		static Activity currcontext;
		static object lockDialog = new object();

		private static void run()
		{
			lock (lockDialog)
			{
				AlertDialog.Builder builder = new AlertDialog.Builder(currcontext);
				builder.SetTitle(Title);
				builder.SetMessage(Message);
				builder.SetIcon(global::Android.Resource.Drawable.IcDialogAlert);
				builder.SetPositiveButton(Button1Text ?? "OK", delegate (object sender, DialogClickEventArgs e)
				{
					result = 1;
				});

				if (!string.IsNullOrEmpty(Button2Text))
				{
					builder.SetNegativeButton(Button2Text, delegate (object sender, DialogClickEventArgs e)
					{
						result = 2;
					});

					if (!string.IsNullOrEmpty(Button3Text))
					{
						builder.SetNeutralButton(Button3Text, delegate (object sender, DialogClickEventArgs e)
						{
							result = 3;
						});
					}
				}
				builder.Show();
			}
		}

		public enum ButtonResult
		{
			ResultButton1, ResultButton2, ResultButton3
		}

		public static ButtonResult Show(Activity context, string title, string message, string button1Text, string button2Text = null, string button3Text = null)
		{
			currcontext = context;
			Title = title;
			Message = message;
			Button1Text = button1Text;
			Button2Text = Button3Text = null;
			if (!string.IsNullOrEmpty(button2Text))
			{
				Button2Text = button2Text;
				if (!string.IsNullOrEmpty(button3Text))
				{
					Button3Text = button3Text;
				}
			}
			context.RunOnUiThread(() => run());
			switch (result)
			{
				case 1:
					return ButtonResult.ResultButton1;
				case 2:
					return ButtonResult.ResultButton2;
				case 3:
					return ButtonResult.ResultButton3;
			}

			return ButtonResult.ResultButton1;
		}

	}
}
