using System;
using Forms = System.Windows.Forms;
using Settings = Kean.Platform.Settings;
using Single = Kean.Math.Geometry2D.Single;

namespace Example.RectangleOverlay
{
	public class Viewer : 
		Forms.UserControl
	{
		Imint.Vidview.Viewer vidview;

		public Viewer()
		{
			this.InitializeComponent();
		}

		void InitializeComponent()
		{
			this.vidview = new Imint.Vidview.Viewer();
			this.SuspendLayout();
			// 
			// viewer
			// 
            this.vidview.Asynchronous = Settings.Asynchronous.SetNotify;
            this.vidview.Arguments = "-r console:// -r telnet://:23";
			this.vidview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.vidview.Location = new System.Drawing.Point(0, 0);
			this.vidview.Name = "viewer";
			this.vidview.SeparateProcess = false;
			this.vidview.Size = new System.Drawing.Size(800, 600);
			this.vidview.TabIndex = 0;
			// When the Vidview viewer is closed force shutdown of the full application.
			this.vidview.Closed += System.Windows.Forms.Application.Exit;
			// When the Vidview viewer is fully initialized open test://photo. In case of errors shut down the viewer and the application.
			this.vidview.Started += () => 
			{
				if (!(this.vidview.Media != null && this.vidview.Media.Open("test://photo")))
					this.vidview.Close();
                (this.vidview.Viewer.Overlays["rectangle1"] as Imint.Viewer.Overlays.IRectangle).Region = new Single.Box(200, 200, 100, 100);
                (this.vidview.Viewer.Overlays["rectangle1"] as Imint.Viewer.Overlays.IRectangle).Fill = new Attraction.Svg.Svg.Types.Paint("gray");

                (this.vidview.Viewer.Overlays["rectangle2"] as Imint.Viewer.Overlays.IRectangle).Region = new Single.Box(250, 250, 150, 75);
                (this.vidview.Viewer.Overlays["rectangle2"] as Imint.Viewer.Overlays.IRectangle).Stroke = new Attraction.Svg.Svg.Types.Paint("purple");
                (this.vidview.Viewer.Overlays["rectangle2"] as Imint.Viewer.Overlays.IRectangle).StrokeWidth = new Attraction.Svg.Svg.Types.Length("10px");

                (this.vidview.Viewer.Overlays["rectangle3"] as Imint.Viewer.Overlays.IRectangle).Region = new Single.Box(500, 300, 50, 150);

                (this.vidview.Viewer.Overlays["rectangle4"] as Imint.Viewer.Overlays.IRectangle).Region = new Single.Box(20, 20, 500, 300);
            };
			this.Controls.Add(this.vidview);
			this.ResumeLayout(false);
		}
	}
}
