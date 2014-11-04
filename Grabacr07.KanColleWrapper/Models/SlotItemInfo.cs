﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Internal;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace Grabacr07.KanColleWrapper.Models
{
	/// <summary>
	/// 装備アイテムの種類に基づく情報を表します。
	/// </summary>
	public class SlotItemInfo : RawDataWrapper<kcsapi_mst_slotitem>, IIdentifiable
	{
		private SlotItemIconType? iconType;
		private int? categoryId;

		public int Id
		{
			get { return this.RawData.api_id; }
		}

		public string Name
		{
			get
			{
				return KanColleClient.Current.Translations.GetTranslation(this.RawData.api_name, TranslationType.Equipment, this.RawData);
			}
		}

		public string UntranslatedName
		{
			get
			{
				return this.RawData.api_name;
			}
		}

		public SlotItemIconType IconType
		{
			get { return this.iconType ?? (SlotItemIconType)(this.iconType = (SlotItemIconType)(this.RawData.api_type.Get(3) ?? 0)); }
		}

		public int CategoryId
		{
			get { return this.categoryId ?? (int)(this.categoryId = this.RawData.api_type.Get(2) ?? int.MaxValue); }
		}

		/// <summary>
		/// 対空値を取得します。
		/// </summary>
		public int AA
		{
			get { return this.RawData.api_tyku; }
		}

		/// <summary>
		/// 制空戦に参加できる戦闘機または水上機かどうかを示す値を取得します。
		/// </summary>
		public bool IsAirSuperiorityFighter
		{
			get
			{
				var type = this.RawData.api_type.Get(2);
				return type.HasValue && (type == 6 || type == 7 || type == 8 || type == 11);
			}
		}

		public int Firepower
		{
			get { return this.RawData.api_houg; }
		}

		public int Torpedo
		{
			get { return this.RawData.api_raig; }
		}

		public int AntiSub
		{
			get { return this.RawData.api_tais; }
		}

		public int SightRange
		{
			get { return this.RawData.api_saku; }
		}

		public int Speed
		{
			get { return this.RawData.api_soku; }
		}

		public int Armor
		{
			get { return this.RawData.api_souk; }
		}

		public int Health
		{
			get { return this.RawData.api_taik; }
		}

		public int Luck
		{
			get { return this.RawData.api_luck; }
		}

		public int Evasion
		{
			get { return this.RawData.api_houk; }
		}

		public int Accuracy
		{
			get { return this.RawData.api_houm; }
		}

		public int DiveBomb
		{
			get { return this.RawData.api_baku; }
		}

		public int AttackRange
		{
			get { return this.RawData.api_leng; }
		}


		internal SlotItemInfo(kcsapi_mst_slotitem rawData) : base(rawData) { }

		public override string ToString()
		{
			return string.Format("ID = {0}, Name = \"{1}\", Type = {{{2}}}", this.Id, this.Name, this.RawData.api_type.ToString(", "));
		}

		#region static members

		private static readonly SlotItemInfo dummy = new SlotItemInfo(new kcsapi_mst_slotitem()
		{
			api_id = 0,
			api_name = "？？？",
		});

		public static SlotItemInfo Dummy
		{
			get { return dummy; }
		}

		#endregion
	}
}
