﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinDmd.Input;
using PinDmd.Output;
using PinDmd.Processor;

namespace PinDmd
{
	public class RenderGraph
	{
		public IFrameSource Source { get; set; }
		public List<IProcessor> Processors { get; set; }
		public List<IFrameDestination> Destinations { get; set; }

		private readonly List<IDisposable> _activeRenderers = new List<IDisposable>();

		public void StartRendering()
		{
			foreach (var dest in Destinations) {
				_activeRenderers.Add(Source.GetFrames().Subscribe(dest.Render));
			}
		}

		public void StopRendering()
		{
			foreach (var activeRenderer in _activeRenderers) {
				activeRenderer.Dispose();
			}
		}

		public void Render(Bitmap bmp)
		{
			foreach (var dest in Destinations) {
				dest.RenderBitmap(bmp);
			}
		}


	}
}