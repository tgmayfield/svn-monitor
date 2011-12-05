using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<Description>j__TPar, <Value>j__TPar>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Description>j__TPar <Description>i__Field;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Value>j__TPar <Value>i__Field;

	public <Description>j__TPar Description
	{
		get
		{
			return this.<Description>i__Field;
		}
	}

	public <Value>j__TPar Value
	{
		get
		{
			return this.<Value>i__Field;
		}
	}

	[DebuggerHidden]
	public <>f__AnonymousType0(<Description>j__TPar Description, <Value>j__TPar Value)
	{
		this.<Description>i__Field = Description;
		this.<Value>i__Field = Value;
	}

	[DebuggerHidden]
	public override bool Equals(object value)
	{
		<>f__AnonymousType0<<Description>j__TPar, <Value>j__TPar> <>f_AnonymousType0 = value as <>f__AnonymousType0<<Description>j__TPar, <Value>j__TPar>;
		if (<>f_AnonymousType0 != null && EqualityComparer<<Description>j__TPar>.Default.Equals(this.<Description>i__Field, <>f_AnonymousType0.<Description>i__Field))
		{
			return EqualityComparer<<Value>j__TPar>.Default.Equals(this.<Value>i__Field, <>f_AnonymousType0.<Value>i__Field);
		}
		return false;
	}

	[DebuggerHidden]
	public override int GetHashCode()
	{
		int hashCode = 864310944;
		hashCode = -1521134295 * hashCode + EqualityComparer<<Description>j__TPar>.Default.GetHashCode(this.<Description>i__Field);
		hashCode = -1521134295 * hashCode + EqualityComparer<<Value>j__TPar>.Default.GetHashCode(this.<Value>i__Field);
		return hashCode;
	}

	[DebuggerHidden]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("{ Description = ");
		stringBuilder.Append(this.<Description>i__Field);
		stringBuilder.Append(", Value = ");
		stringBuilder.Append(this.<Value>i__Field);
		stringBuilder.Append(" }");
		return stringBuilder.ToString();
	}
}