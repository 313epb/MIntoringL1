// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Task
{
	public class SampleHarness : IEnumerable<Sample>
	{
		private readonly IDictionary<int, Sample> _samples = new Dictionary<int, Sample>();


		public SampleHarness()
		{
			var samplesType = GetType();

			Title = "Samples";
			var prefix = "Sample";
			string codeFile = samplesType.Name + ".cs";

			foreach (Attribute a in samplesType.GetCustomAttributes(false))
			{
				if (a is TitleAttribute)
					Title = ((TitleAttribute) a).Title;
				else if (a is PrefixAttribute)
					prefix = ((PrefixAttribute) a).Prefix;
			}

			string allCode = ReadFile(Application.StartupPath + @"\..\..\" + codeFile);

			var methods =
				from sm in samplesType.GetMethods(BindingFlags.Public | BindingFlags.Instance |
												BindingFlags.DeclaredOnly | BindingFlags.Static)
				where sm.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
				orderby sm.MetadataToken
				select sm;

			var m = 1;
			foreach (var method in methods)
			{
				var methodCategory = "Miscellaneous";
				string methodTitle = prefix + " Sample " + m;
				var methodDescription = "See code.";
				var linkedMethods = new List<MethodInfo>();
				var linkedClasses = new List<Type>();

				foreach (Attribute a in method.GetCustomAttributes(false))
				{
					if (a is CategoryAttribute)
						methodCategory = ((CategoryAttribute) a).Category;
					else if (a is TitleAttribute)
						methodTitle = ((TitleAttribute) a).Title;
					else if (a is DescriptionAttribute)
						methodDescription = ((DescriptionAttribute) a).Description;
					else if (a is LinkedMethodAttribute)
					{
						var linked = samplesType.GetMethod(((LinkedMethodAttribute) a).MethodName,
							BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
						if (linked != null)
							linkedMethods.Add(linked);
					}
					else if (a is LinkedClassAttribute)
					{
						var linked = samplesType.GetNestedType(((LinkedClassAttribute) a).ClassName);
						if (linked != null)
							linkedClasses.Add(linked);
					}
				}

				var methodCode = new StringBuilder();
				methodCode.Append(GetCodeBlock(allCode, "void " + method.Name));

				foreach (var lm in linkedMethods)
				{
					methodCode.Append(Environment.NewLine);
					methodCode.Append(GetCodeBlock(allCode, ShortTypeName(lm.ReturnType.FullName) + " " + lm.Name));
				}

				foreach (var lt in linkedClasses)
				{
					methodCode.Append(Environment.NewLine);
					methodCode.Append(GetCodeBlock(allCode, "class " + lt.Name));
				}

				var sample = new Sample(this, method, methodCategory, methodTitle, methodDescription,
					methodCode.ToString());

				_samples.Add(m, sample);
				m++;
			}
		}


		public string Title { get; }

		public StreamWriter OutputStreamWriter { get; } = new StreamWriter(new MemoryStream());


		public Sample this[int index] => _samples[index];

		IEnumerator<Sample> IEnumerable<Sample>.GetEnumerator()
		{
			return _samples.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _samples.Values.GetEnumerator();
		}

		private static string ReadFile(string path)
		{
			string fileContents;
			if (File.Exists(path))
				using (var reader = File.OpenText(path))
					fileContents = reader.ReadToEnd();
			else
				fileContents = "";

			return fileContents;
		}

		private static string ShortTypeName(string typeName)
		{
			bool isAssemblyQualified = typeName[0] == '[';
			if (isAssemblyQualified)
			{
				int commaPos = typeName.IndexOf(',');
				return ShortTypeName(typeName.Substring(1, commaPos - 1));
			}
			bool isGeneric = typeName.Contains("`");
			if (isGeneric)
			{
				int backTickPos = typeName.IndexOf('`');
				int leftBracketPos = typeName.IndexOf('[');
				string typeParam =
					ShortTypeName(typeName.Substring(leftBracketPos + 1, typeName.Length - leftBracketPos - 2));
				return ShortTypeName(typeName.Substring(0, backTickPos)) + "<" + typeParam + ">";
			}
			switch (typeName)
			{
				case "System.Void":
					return "void";
				case "System.Int16":
					return "short";
				case "System.Int32":
					return "int";
				case "System.Int64":
					return "long";
				case "System.Single":
					return "float";
				case "System.Double":
					return "double";
				case "System.String":
					return "string";
				case "System.Char":
					return "char";
				case "System.Boolean":
					return "bool";

				/* other primitive types omitted */

				default:
					int lastDotPos = typeName.LastIndexOf('.');
					int lastPlusPos = typeName.LastIndexOf('+');
					int startPos = Math.Max(lastDotPos, lastPlusPos) + 1;
					return typeName.Substring(startPos, typeName.Length - startPos);
			}
		}

		private static string GetCodeBlock(string allCode, string blockName)
		{
			int blockStart = allCode.IndexOf(blockName, StringComparison.OrdinalIgnoreCase);

			if (blockStart == -1)
				return "// " + blockName + " code not found";
			blockStart = allCode.LastIndexOf(Environment.NewLine, blockStart, StringComparison.OrdinalIgnoreCase);
			if (blockStart == -1)
				blockStart = 0;
			else
				blockStart += Environment.NewLine.Length;

			int pos = blockStart;
			var braceCount = 0;
			char c;
			do
			{
				pos++;

				c = allCode[pos];
				switch (c)
				{
					case '{':
						braceCount++;
						break;

					case '}':
						braceCount--;
						break;
				}
			} while (pos < allCode.Length && !(c == '}' && braceCount == 0));

			int blockEnd = pos;

			string blockCode = allCode.Substring(blockStart, blockEnd - blockStart + 1);

			return RemoveIndent(blockCode);
		}

		private static string RemoveIndent(string code)
		{
			var indentSpaces = 0;
			while (code[indentSpaces] == ' ')
			{
				indentSpaces++;
			}

			var builder = new StringBuilder();
			var codeLines = code.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			foreach (string line in codeLines)
			{
				if (indentSpaces < line.Length)
					builder.AppendLine(line.Substring(indentSpaces));
				else
					builder.AppendLine();
			}

			return builder.ToString();
		}


		public virtual void InitSample()
		{
		}

		public virtual void HandleException(Exception e)
		{
			Console.Write(e);
		}


		public void RunAllSamples()
		{
			var oldConsoleOut = Console.Out;
			Console.SetOut(StreamWriter.Null);

			foreach (var sample in this)
			{
				sample.Invoke();
			}

			Console.SetOut(oldConsoleOut);
		}
	}

	public class Sample
	{
		public Sample(SampleHarness harness, MethodInfo method, string category, string title,
			string description, string code)
		{
			Harness = harness;
			Method = method;
			Category = category;
			Title = title;
			Description = description;
			Code = code;
		}


		public SampleHarness Harness { get; }

		public MethodInfo Method { get; }

		public string Category { get; }

		public string Title { get; }

		public string Description { get; }

		public string Code { get; }


		public void Invoke()
		{
			Harness.InitSample();
			Method.Invoke(Harness, null);
		}

		public void InvokeSafe()
		{
			try
			{
				Invoke();
			}
			catch (TargetInvocationException e)
			{
				Harness.HandleException(e.InnerException);
			}
		}

		public override string ToString()
		{
			return Title;
		}
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public sealed class TitleAttribute : Attribute
	{
		public TitleAttribute(string title)
		{
			Title = title;
		}

		public string Title { get; set; }
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public sealed class PrefixAttribute : Attribute
	{
		public PrefixAttribute(string prefix)
		{
			Prefix = prefix;
		}

		public string Prefix { get; set; }
	}

	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CategoryAttribute : Attribute
	{
		public CategoryAttribute(string category)
		{
			Category = category;
		}

		public string Category { get; set; }
	}

	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DescriptionAttribute : Attribute
	{
		public DescriptionAttribute(string description)
		{
			Description = description;
		}

		public string Description { get; set; }
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class LinkedMethodAttribute : Attribute
	{
		public LinkedMethodAttribute(string methodName)
		{
			MethodName = methodName;
		}

		public string MethodName { get; set; }
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class LinkedClassAttribute : Attribute
	{
		public LinkedClassAttribute(string className)
		{
			ClassName = className;
		}

		public string ClassName { get; set; }
	}
}