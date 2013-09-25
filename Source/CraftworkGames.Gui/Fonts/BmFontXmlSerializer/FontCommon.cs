using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CraftworkGames.Gui
{
	// ---- AngelCode BmFont XML serializer ----------------------
	// ---- By DeadlyDan @ deadlydan@gmail.com -------------------
	// ---- There's no license restrictions, use as you will. ----
	// ---- Credits to http://www.angelcode.com/ -----------------	
	[DataContractAttribute]
	public class FontCommon
	{
        [DataMember(Name = "lineHeight")]
		public Int32 LineHeight
		{
			get;
			set;
		}

        [DataMember(Name = "base")]
		public Int32 Base
		{
			get;
			set;
		}

        [DataMember(Name = "scaleW")]
		public Int32 ScaleW
		{
			get;
			set;
		}

        [DataMember(Name = "scaleH")]
		public Int32 ScaleH
		{
			get;
			set;
		}

        [DataMember(Name = "pages")]
		public Int32 Pages
		{
			get;
			set;
		}

        [DataMember(Name = "packed")]
		public Int32 Packed
		{
			get;
			set;
		}

        [DataMember(Name = "alphaChnl")]
		public Int32 AlphaChannel
		{
			get;
			set;
		}

        [DataMember(Name = "redChnl")]
		public Int32 RedChannel
		{
			get;
			set;
		}

        [DataMember(Name = "greenChnl")]
		public Int32 GreenChannel
		{
			get;
			set;
		}

        [DataMember(Name = "blueChnl")]
		public Int32 BlueChannel
		{
			get;
			set;
		}
	}	
}
