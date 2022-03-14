// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Task
{
	internal partial class SampleForm : Form
	{
		private SampleHarness _currentHarness;
		private Sample _currentSample;

		public SampleForm(string title, List<SampleHarness> harnesses)
		{
			InitializeComponent();

			Text = title;

			var rootNode = new TreeNode(title)
			{
				Tag = null,
				ImageKey = "Help",
				SelectedImageKey = "Help"
			};
			samplesTreeView.Nodes.Add(rootNode);
			rootNode.Expand();

			foreach (var harness in harnesses)
			{
				var harnessNode = new TreeNode(harness.Title)
				{
					Tag = null,
					ImageKey = "BookStack",
					SelectedImageKey = "BookStack"
				};
				rootNode.Nodes.Add(harnessNode);

				var category = "";
				TreeNode categoryNode = null;
				foreach (var sample in harness)
				{
					if (sample.Category != category)
					{
						category = sample.Category;

						categoryNode = new TreeNode(category)
						{
							Tag = null,
							ImageKey = "BookClosed",
							SelectedImageKey = "BookClosed"
						};
						harnessNode.Nodes.Add(categoryNode);
					}

					var node = new TreeNode(sample.ToString())
					{
						Tag = sample,
						ImageKey = "Item",
						SelectedImageKey = "Item"
					};
					categoryNode.Nodes.Add(node);
				}
			}
		}


		private void samplesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var currentNode = samplesTreeView.SelectedNode;
			_currentSample = (Sample) currentNode.Tag;
			if (_currentSample != null)
			{
				_currentHarness = _currentSample.Harness;
				runButton.Enabled = true;
				descriptionTextBox.Text = _currentSample.Description;
				codeRichTextBox.Clear();
				codeRichTextBox.Text = _currentSample.Code;
				ColorizeCode(codeRichTextBox);
				outputTextBox.Clear();
			}
			else
			{
				_currentHarness = null;
				runButton.Enabled = false;
				descriptionTextBox.Text = "Select a query from the tree to the left.";
				codeRichTextBox.Clear();
				outputTextBox.Clear();
				if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Unknown)
					e.Node.Expand();
			}
		}

		private void samplesTreeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			switch (e.Node.Level)
			{
				case 1:
				case 2:
					e.Node.ImageKey = "BookOpen";
					e.Node.SelectedImageKey = "BookOpen";
					break;
			}
		}

		private void samplesTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			switch (e.Node.Level)
			{
				case 0:
					e.Cancel = true;
					break;
			}
		}

		private void samplesTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			switch (e.Node.Level)
			{
				case 1:
					e.Node.ImageKey = "BookStack";
					e.Node.SelectedImageKey = "BookStack";
					break;

				case 2:
					e.Node.ImageKey = "BookClosed";
					e.Node.SelectedImageKey = "BookClosed";
					break;
			}
		}

		private void samplesTreeView_DoubleClick(object sender, EventArgs e)
		{
			if (_currentSample != null)
			{
				RunCurrentSample();
			}
		}

		private static void ColorizeCode(RichTextBox rtb)
		{
			string[] keywords =
			{
				"as", "do", "if", "in", "is", "for", "int", "new", "out", "ref", "try", "base",
				"bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long", "null",
				"this", "true", "uint", "void", "break", "catch", "class", "const", "event", "false",
				"fixed", "float", "sbyte", "short", "throw", "ulong", "using", "where", "while",
				"yield", "double", "extern", "object", "params", "public", "return", "sealed",
				"sizeof", "static", "string", "struct", "switch", "typeof", "unsafe", "ushort",
				"checked", "decimal", "default", "finally", "foreach", "partial", "private",
				"virtual", "abstract", "continue", "delegate", "explicit", "implicit", "internal",
				"operator", "override", "readonly", "volatile",
				"interface", "namespace", "protected", "unchecked",
				"stackalloc",
				"from", "in", "where", "select", "join", "equals", "let", "on", "group", "by",
				"into", "orderby", "ascending", "descending", "var"
			};
			string text = rtb.Text;

			rtb.SelectAll();
			rtb.SelectionColor = rtb.ForeColor;

			foreach (string keyword in keywords)
			{
				int keywordPos = rtb.Find(keyword, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
				while (keywordPos != -1)
				{
					int commentPos = text.LastIndexOf("//", keywordPos, StringComparison.OrdinalIgnoreCase);
					int newLinePos = text.LastIndexOf("\n", keywordPos, StringComparison.OrdinalIgnoreCase);
					var quoteCount = 0;
					int quotePos = text.IndexOf("\"", newLinePos + 1, keywordPos - newLinePos,
						StringComparison.OrdinalIgnoreCase);
					while (quotePos != -1)
					{
						quoteCount++;
						quotePos = text.IndexOf("\"", quotePos + 1, keywordPos - (quotePos + 1),
							StringComparison.OrdinalIgnoreCase);
					}

					if (newLinePos >= commentPos && quoteCount%2 == 0)
						rtb.SelectionColor = Color.Blue;

					keywordPos = rtb.Find(keyword, keywordPos + rtb.SelectionLength,
						RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
				}
			}

			rtb.Select(0, 0);
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			RunCurrentSample();
		}

		private void RunCurrentSample()
		{
			var hold = Cursor;
			Cursor = Cursors.WaitCursor;

			outputTextBox.Text = "";
			var writer = _currentHarness.OutputStreamWriter;
			var oldConsoleOut = Console.Out;
			Console.SetOut(writer);
			var stream = (MemoryStream) writer.BaseStream;
			stream.SetLength(0);

			_currentSample.InvokeSafe();

			writer.Flush();
			Console.SetOut(oldConsoleOut);
			outputTextBox.Text += writer.Encoding.GetString(stream.ToArray());

			Cursor = hold;
		}
	}
}