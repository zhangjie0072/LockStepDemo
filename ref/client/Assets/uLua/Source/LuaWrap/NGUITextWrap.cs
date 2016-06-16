using System;
using UnityEngine;
using LuaInterface;

public class NGUITextWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Update", Update),
			new LuaMethod("Prepare", Prepare),
			new LuaMethod("GetSymbol", GetSymbol),
			new LuaMethod("GetGlyphWidth", GetGlyphWidth),
			new LuaMethod("GetGlyph", GetGlyph),
			new LuaMethod("ParseColor", ParseColor),
			new LuaMethod("ParseColor24", ParseColor24),
			new LuaMethod("ParseColor32", ParseColor32),
			new LuaMethod("EncodeColor", EncodeColor),
			new LuaMethod("EncodeColor24", EncodeColor24),
			new LuaMethod("EncodeColor32", EncodeColor32),
			new LuaMethod("ParseSymbol", ParseSymbol),
			new LuaMethod("StripSymbols", StripSymbols),
			new LuaMethod("Align", Align),
			new LuaMethod("GetClosestCharacter", GetClosestCharacter),
			new LuaMethod("EndLine", EndLine),
			new LuaMethod("CalculatePrintedSize", CalculatePrintedSize),
			new LuaMethod("CalculateOffsetToFit", CalculateOffsetToFit),
			new LuaMethod("GetEndOfLineThatFits", GetEndOfLineThatFits),
			new LuaMethod("WrapText", WrapText),
			new LuaMethod("Print", Print),
			new LuaMethod("PrintCharacterPositions", PrintCharacterPositions),
			new LuaMethod("PrintCaretAndSelection", PrintCaretAndSelection),
			new LuaMethod("New", _CreateNGUIText),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("bitmapFont", get_bitmapFont, set_bitmapFont),
			new LuaField("dynamicFont", get_dynamicFont, set_dynamicFont),
			new LuaField("glyph", get_glyph, set_glyph),
			new LuaField("fontSize", get_fontSize, set_fontSize),
			new LuaField("fontScale", get_fontScale, set_fontScale),
			new LuaField("pixelDensity", get_pixelDensity, set_pixelDensity),
			new LuaField("fontStyle", get_fontStyle, set_fontStyle),
			new LuaField("alignment", get_alignment, set_alignment),
			new LuaField("tint", get_tint, set_tint),
			new LuaField("rectWidth", get_rectWidth, set_rectWidth),
			new LuaField("rectHeight", get_rectHeight, set_rectHeight),
			new LuaField("maxLines", get_maxLines, set_maxLines),
			new LuaField("gradient", get_gradient, set_gradient),
			new LuaField("gradientBottom", get_gradientBottom, set_gradientBottom),
			new LuaField("gradientTop", get_gradientTop, set_gradientTop),
			new LuaField("encoding", get_encoding, set_encoding),
			new LuaField("spacingX", get_spacingX, set_spacingX),
			new LuaField("spacingY", get_spacingY, set_spacingY),
			new LuaField("premultiply", get_premultiply, set_premultiply),
			new LuaField("symbolStyle", get_symbolStyle, set_symbolStyle),
			new LuaField("finalSize", get_finalSize, set_finalSize),
			new LuaField("finalSpacingX", get_finalSpacingX, set_finalSpacingX),
			new LuaField("finalLineHeight", get_finalLineHeight, set_finalLineHeight),
			new LuaField("baseline", get_baseline, set_baseline),
			new LuaField("useSymbols", get_useSymbols, set_useSymbols),
		};

		LuaScriptMgr.RegisterLib(L, "NGUIText", typeof(NGUIText), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNGUIText(IntPtr L)
	{
		LuaDLL.luaL_error(L, "NGUIText class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(NGUIText);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bitmapFont(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.bitmapFont);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dynamicFont(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.dynamicFont);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_glyph(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, NGUIText.glyph);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fontSize(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.fontSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fontScale(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.fontScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelDensity(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.pixelDensity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fontStyle(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.fontStyle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alignment(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.alignment);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tint(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.tint);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rectWidth(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.rectWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rectHeight(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.rectHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxLines(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.maxLines);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gradient(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.gradient);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gradientBottom(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.gradientBottom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gradientTop(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.gradientTop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_encoding(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.encoding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spacingX(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.spacingX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spacingY(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.spacingY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_premultiply(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.premultiply);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_symbolStyle(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.symbolStyle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_finalSize(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.finalSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_finalSpacingX(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.finalSpacingX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_finalLineHeight(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.finalLineHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_baseline(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.baseline);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useSymbols(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.useSymbols);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bitmapFont(IntPtr L)
	{
		NGUIText.bitmapFont = (UIFont)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIFont));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dynamicFont(IntPtr L)
	{
		NGUIText.dynamicFont = (Font)LuaScriptMgr.GetUnityObject(L, 3, typeof(Font));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_glyph(IntPtr L)
	{
		NGUIText.glyph = (NGUIText.GlyphInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(NGUIText.GlyphInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fontSize(IntPtr L)
	{
		NGUIText.fontSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fontScale(IntPtr L)
	{
		NGUIText.fontScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelDensity(IntPtr L)
	{
		NGUIText.pixelDensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fontStyle(IntPtr L)
	{
		NGUIText.fontStyle = (FontStyle)LuaScriptMgr.GetNetObject(L, 3, typeof(FontStyle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alignment(IntPtr L)
	{
		NGUIText.alignment = (NGUIText.Alignment)LuaScriptMgr.GetNetObject(L, 3, typeof(NGUIText.Alignment));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tint(IntPtr L)
	{
		NGUIText.tint = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rectWidth(IntPtr L)
	{
		NGUIText.rectWidth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rectHeight(IntPtr L)
	{
		NGUIText.rectHeight = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxLines(IntPtr L)
	{
		NGUIText.maxLines = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gradient(IntPtr L)
	{
		NGUIText.gradient = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gradientBottom(IntPtr L)
	{
		NGUIText.gradientBottom = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gradientTop(IntPtr L)
	{
		NGUIText.gradientTop = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_encoding(IntPtr L)
	{
		NGUIText.encoding = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spacingX(IntPtr L)
	{
		NGUIText.spacingX = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spacingY(IntPtr L)
	{
		NGUIText.spacingY = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_premultiply(IntPtr L)
	{
		NGUIText.premultiply = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_symbolStyle(IntPtr L)
	{
		NGUIText.symbolStyle = (NGUIText.SymbolStyle)LuaScriptMgr.GetNetObject(L, 3, typeof(NGUIText.SymbolStyle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_finalSize(IntPtr L)
	{
		NGUIText.finalSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_finalSpacingX(IntPtr L)
	{
		NGUIText.finalSpacingX = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_finalLineHeight(IntPtr L)
	{
		NGUIText.finalLineHeight = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_baseline(IntPtr L)
	{
		NGUIText.baseline = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useSymbols(IntPtr L)
	{
		NGUIText.useSymbols = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NGUIText.Update();
			return 0;
		}
		else if (count == 1)
		{
			bool arg0 = LuaScriptMgr.GetBoolean(L, 1);
			NGUIText.Update(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NGUIText.Update");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Prepare(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		NGUIText.Prepare(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSymbol(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
		BMSymbol o = NGUIText.GetSymbol(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGlyphWidth(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		float o = NGUIText.GetGlyphWidth(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGlyph(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		NGUIText.GlyphInfo o = NGUIText.GetGlyph(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		Color o = NGUIText.ParseColor(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseColor24(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		Color o = NGUIText.ParseColor24(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseColor32(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		Color o = NGUIText.ParseColor32(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EncodeColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Color arg0 = LuaScriptMgr.GetColor(L, 1);
		string o = NGUIText.EncodeColor(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EncodeColor24(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Color arg0 = LuaScriptMgr.GetColor(L, 1);
		string o = NGUIText.EncodeColor24(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EncodeColor32(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Color arg0 = LuaScriptMgr.GetColor(L, 1);
		string o = NGUIText.EncodeColor32(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseSymbol(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNetObject(L, 2, typeof(int));
			bool o = NGUIText.ParseSymbol(arg0,ref arg1);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 9)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNetObject(L, 2, typeof(int));
			BetterList<Color> arg2 = (BetterList<Color>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<Color>));
			bool arg3 = LuaScriptMgr.GetBoolean(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNetObject(L, 5, typeof(int));
			bool arg5 = (bool)LuaScriptMgr.GetNetObject(L, 6, typeof(bool));
			bool arg6 = (bool)LuaScriptMgr.GetNetObject(L, 7, typeof(bool));
			bool arg7 = (bool)LuaScriptMgr.GetNetObject(L, 8, typeof(bool));
			bool arg8 = (bool)LuaScriptMgr.GetNetObject(L, 9, typeof(bool));
			bool o = NGUIText.ParseSymbol(arg0,ref arg1,arg2,arg3,ref arg4,ref arg5,ref arg6,ref arg7,ref arg8);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			LuaScriptMgr.Push(L, arg4);
			LuaScriptMgr.Push(L, arg5);
			LuaScriptMgr.Push(L, arg6);
			LuaScriptMgr.Push(L, arg7);
			LuaScriptMgr.Push(L, arg8);
			return 7;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NGUIText.ParseSymbol");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StripSymbols(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = NGUIText.StripSymbols(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Align(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BetterList<Vector3> arg0 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 1, typeof(BetterList<Vector3>));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
		NGUIText.Align(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClosestCharacter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BetterList<Vector3> arg0 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 1, typeof(BetterList<Vector3>));
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 2);
		int o = NGUIText.GetClosestCharacter(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndLine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		System.Text.StringBuilder arg0 = (System.Text.StringBuilder)LuaScriptMgr.GetNetObject(L, 1, typeof(System.Text.StringBuilder));
		NGUIText.EndLine(ref arg0);
		LuaScriptMgr.PushObject(L, arg0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculatePrintedSize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Vector2 o = NGUIText.CalculatePrintedSize(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateOffsetToFit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int o = NGUIText.CalculateOffsetToFit(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEndOfLineThatFits(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = NGUIText.GetEndOfLineThatFits(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WrapText(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			string arg1 = null;
			bool o = NGUIText.WrapText(arg0,out arg1);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			string arg1 = null;
			bool arg2 = LuaScriptMgr.GetBoolean(L, 3);
			bool o = NGUIText.WrapText(arg0,out arg1,arg2);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NGUIText.WrapText");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Print(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		BetterList<Vector3> arg1 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 2, typeof(BetterList<Vector3>));
		BetterList<Vector2> arg2 = (BetterList<Vector2>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<Vector2>));
		BetterList<Color32> arg3 = (BetterList<Color32>)LuaScriptMgr.GetNetObject(L, 4, typeof(BetterList<Color32>));
		NGUIText.Print(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrintCharacterPositions(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		BetterList<Vector3> arg1 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 2, typeof(BetterList<Vector3>));
		BetterList<int> arg2 = (BetterList<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<int>));
		NGUIText.PrintCharacterPositions(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrintCaretAndSelection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
		BetterList<Vector3> arg3 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 4, typeof(BetterList<Vector3>));
		BetterList<Vector3> arg4 = (BetterList<Vector3>)LuaScriptMgr.GetNetObject(L, 5, typeof(BetterList<Vector3>));
		NGUIText.PrintCaretAndSelection(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}
}

