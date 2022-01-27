﻿using System;
using BlitzedConfuser.Utils;
using BlitzedConfuser.Utils.Analyzer;
using dnlib.DotNet;

namespace BlitzedConfuser.Protections
{
	// Token: 0x0200001B RID: 27
	public class Renamer : Protection
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00004AE0 File Offset: 0x00002EE0
		public Renamer()
		{
			base.Name = "Renamer";
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004AF3 File Offset: 0x00002EF3
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00004AFB File Offset: 0x00002EFB
		private int MethodAmount { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004B04 File Offset: 0x00002F04
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00004B0C File Offset: 0x00002F0C
		private int ParameterAmount { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004B15 File Offset: 0x00002F15
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004B1D File Offset: 0x00002F1D
		private int PropertyAmount { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004B26 File Offset: 0x00002F26
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004B2E File Offset: 0x00002F2E
		private int FieldAmount { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004B37 File Offset: 0x00002F37
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00004B3F File Offset: 0x00002F3F
		private int EventAmount { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00004B48 File Offset: 0x00002F48
		public override void Execute()
		{
			if (Kappa.DontRename)
			{
				return;
			}
			Kappa.Module.Mvid = new Guid?(Guid.NewGuid());
			Kappa.Module.EncId = new Guid?(Guid.NewGuid());
			Kappa.Module.EncBaseId = new Guid?(Guid.NewGuid());
			Kappa.Module.Name = "urnotfinnacrackthisretardlolSTONEDEAGLE" + Randomizer.String(12);
			Kappa.Module.EntryPoint.Name = Randomizer.String(MemberRenamer.StringLength()) + "StvnedEagleWINNING";
			foreach (TypeDef typeDef in Kappa.Module.Types)
			{
				if (Renamer.CanRename(typeDef))
				{
					typeDef.Namespace = string.Empty;
					typeDef.Name = "STONEDEAGLEWINNINGLOLSTONEDEAGLEWINNINGLOLSTONEDEAGLEWINNINGLOL" + Randomizer.String(5);
				}
				foreach (MethodDef methodDef in typeDef.Methods)
				{
					if (Renamer.CanRename(methodDef) && !Kappa.ForceWinForms && !Kappa.FileExtension.Contains("dll"))
					{
						methodDef.Name = "STONEDEAGLEWINNINGLOLSTONEDEAGLEWINNINGLOLSTONEDEAGLEWINNINGLOLSTONEDEAGLEWINNINGLOL" + Randomizer.String(6);
						int num = this.MethodAmount + 1;
						this.MethodAmount = num;
					}
					foreach (Parameter parameter in methodDef.Parameters)
					{
						if (Renamer.CanRename(parameter))
						{
							parameter.Name = "STVNEDEAGLE" + Randomizer.String(7);
							int num = this.ParameterAmount + 1;
							this.ParameterAmount = num;
						}
					}
				}
				foreach (PropertyDef propertyDef in typeDef.Properties)
				{
					if (Renamer.CanRename(propertyDef))
					{
						propertyDef.Name = Randomizer.String(MemberRenamer.StringLength()) + "StvnedEagle";
						int num = this.PropertyAmount + 1;
						this.PropertyAmount = num;
					}
				}
				foreach (FieldDef fieldDef in typeDef.Fields)
				{
					if (Renamer.CanRename(fieldDef))
					{
						fieldDef.Name = "STONEREAGLEZLOLSTONEREAGLEZLOLSTONEREAGLEZLOLSTONEREAGLEZLOLSTONEREAGLEZLOL" + Randomizer.String(15);
						int num = this.FieldAmount + 1;
						this.FieldAmount = num;
					}
				}
				foreach (EventDef eventDef in typeDef.Events)
				{
					if (Renamer.CanRename(eventDef))
					{
						eventDef.Name = "StvnedEagleStvnedEagleStvnedEagleStvnedEagleStvnedEagleStvnedEagleStvnedEagleStvnedEagle" + Randomizer.String(6);
						int num = this.EventAmount + 1;
						this.EventAmount = num;
					}
				}
			}
			Console.WriteLine(string.Format("  Renamed {0} methods.\n  Renamed {1} parameters.", this.MethodAmount, this.ParameterAmount) + string.Format("\n  Renamed {0} properties.\n  Renamed {1} fields.\n  Renamed {2} events.", this.PropertyAmount, this.FieldAmount, this.EventAmount));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004F48 File Offset: 0x00003348
		public static bool CanRename(object obj)
		{
			DefAnalyzer defAnalyzer;
			if (obj is MethodDef)
			{
				defAnalyzer = new MethodDefAnalyzer();
			}
			else if (obj is PropertyDef)
			{
				defAnalyzer = new PropertyDefAnalyzer();
			}
			else if (obj is EventDef)
			{
				defAnalyzer = new EventDefAnalyzer();
			}
			else if (obj is FieldDef)
			{
				defAnalyzer = new FieldDefAnalyzer();
			}
			else if (obj is Parameter)
			{
				defAnalyzer = new ParameterAnalyzer();
			}
			else
			{
				if (!(obj is TypeDef))
				{
					return false;
				}
				defAnalyzer = new TypeDefAnalyzer();
			}
			return defAnalyzer.Execute(obj);
		}
	}
}
