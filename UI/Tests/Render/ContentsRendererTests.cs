using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class ContentsRendererTests : RendererTestsBase {
		[TestMethod]
		public void GetControlsPerRow_OneRow_ReturnsMinusOne() {
			ScrollableControl target = GetRenderTargetControl();
			target.Width = 300;
			ContentsRenderer renderer = GetContentsRenderer(target);
			target.Controls.AddRange(new Control[] {
				new Control { Width = 120 },
				new Control { Width = 120 }
			});

			int controlsPerRow = renderer.GetControlsPerRow();

			Assert.AreEqual(-1, controlsPerRow, $"{nameof(renderer.GetControlsPerRow)}() should return -1 when there is only one row.");
		}

		[TestMethod]
		public void GetControlsPerRow_MultipleRows_ReturnsControlsPerRow() {
			ScrollableControl target = GetRenderTargetControl();
			target.Width = 300;
			ContentsRenderer renderer = GetContentsRenderer(target);
			target.Controls.AddRange(new Control[] {
				new Control { Width = 120 },
				new Control { Width = 120 },
				new Control { Width = 120 }
			});

			int controlsPerRow = renderer.GetControlsPerRow();

			Assert.AreEqual(2, controlsPerRow, $"{nameof(renderer.GetControlsPerRow)}() should return the number of controls that fit on a row.");
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException), nameof(ContentsRenderer.GetSelected) + "() should throw " + nameof(InvalidOperationException) + " when nothing is selected.")]
		public void GetSelected_NothingSelected_ThrowsException() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);

			renderer.GetSelected();
		}

		[TestMethod]
		public void GetSelected_OneSelected_ReturnsSelectedControl() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			Control expectedSelected = new Control { BackColor = SystemColors.Highlight };
			target.Controls.AddRange(new Control[] {
				new Control(),
				expectedSelected,
				new Control()
			});

			Control actualSelected = renderer.GetSelected();

			Assert.AreEqual(expectedSelected, actualSelected, $"{nameof(renderer.GetSelected)}() should return the control with the highlight background color.");
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException), nameof(ContentsRenderer.GetSelectedIndex) + "() should throw " + nameof(InvalidOperationException) + " when nothing is selected.")]
		public void GetSelectedIndex_NothingSelected_ThrowsException() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);

			renderer.GetSelectedIndex();
		}

		[TestMethod]
		public void GetSelectedIndex_OneSelected_ReturnsIndexOfSelectedControl() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			target.Controls.AddRange(new Control[] {
				new Control(),
				new Control { BackColor = SystemColors.Highlight },
				new Control()
			});

			int selected = renderer.GetSelectedIndex();

			Assert.AreEqual(1, selected, $"{nameof(renderer.GetSelectedIndex)}() should return the index of the control with the highlight background color.");
		}

		[TestMethod]
		public void UpdateSelected_Control_ChangesSelection() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			target.Controls.AddRange(new Control[] {
				new Control(),
				new Control { BackColor = SystemColors.Highlight },
				new Control()
			});

			renderer.UpdateSelected(target.Controls[2]);

			Assert.AreEqual(target.Controls[2], renderer.GetSelected(), $"{nameof(renderer.UpdateSelected)}() should change the selected control to the one specified.");
		}

		[TestMethod]
		public void UpdateSelected_Index_ChangesSelection() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			target.Controls.AddRange(new Control[] {
				new Control(),
				new Control { BackColor = SystemColors.Highlight },
				new Control()
			});

			renderer.UpdateSelected(2);

			Assert.AreEqual(2, renderer.GetSelectedIndex(), $"{nameof(renderer.UpdateSelected)}() should change the selected control to the one at the specified index.");
		}
	}
}
