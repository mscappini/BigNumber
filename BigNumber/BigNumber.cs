using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BigNumber
{
	public class BigNumber : ICloneable, IComparable<BigNumber>, IEnumerable<byte>, IEquatable<BigNumber>, IFormattable
	{
		#region Fields

		private LinkedList<byte> digits;
		private bool isNegative;

		#endregion

		#region Ctors

		public BigNumber()
		{
			this.digits = new LinkedList<byte>();
		}

		public BigNumber(byte value)
			: this((long)value)
		{ }

		public BigNumber(short value)
			: this((long)value)
		{ }

		public BigNumber(int value)
			: this((long)value)
		{ }

		public BigNumber(long value)
			: this()
		{
			if (value < 0)
			{
				this.isNegative = true;
				value *= -1;
			}
			if (value > 0)
			{
				foreach (byte digit in this.GetDigits(value))
				{
					this.digits.AddLast(digit);
				}
			}
		}

		#endregion

		// The idea of the conditions located in these operations is to use 
		// traditional math rules as shortcuts to determine if a value should 
		// be negative by the time the operation is complete.

		#region Addition

		public BigNumber Add(long value)
		{
			return Add(new BigNumber(value));
		}

		public BigNumber Add(BigNumber counter)
		{
			return Add(this, counter);
		}

		private static BigNumber Add(BigNumber a, BigNumber b)
		{
			if (b.IsZero())
				return a.Clone() as BigNumber;

			if (a.isNegative && b.isNegative)
			{
				BigNumber aAbs = a.Abs(), bAbs = b.Abs();
				BigNumber c = Add(aAbs, bAbs);
				c.isNegative = true;
				return c;
			}
			else if (a.isNegative != b.isNegative)
			{
				BigNumber aAbs = a.Abs(), bAbs = b.Abs();
				BigNumber c = Subtract(aAbs, bAbs);
				c.isNegative = (a.isNegative && !b.isNegative && aAbs > bAbs) ||
					(!a.isNegative && b.isNegative && aAbs < bAbs);
				return c;
			}
			else
			{
				BigNumber c = a.Clone() as BigNumber;

				LinkedListNode<byte> first, second;
				first = c.digits.First;
				second = b.digits.First;

				bool carry = false;
				while (first != null || second != null || carry)
				{
					if (first == null && second == null && carry)
					{
						// A scenario where the result is greater than either size of digits existing (99 + 1).
						first = c.digits.AddLast(1);
						carry = false;
					}
					else
					{
						if (first != null && second != null)
						{
							// A scenario where the current digit place exists in both numbers.
							first.Value += second.Value;
						}
						else if (first == null && second != null)
						{
							// A scenario where the additional number is larger than the existing number.
							first = c.digits.AddLast(second.Value);
						}

						// If a digit was carried, add one.
						if (carry)
							first.Value++;

						// Determine if another digit is to be carried.
						carry = (first.Value >= 10);

						// If a digit is to be carried, get the remainder from 10.
						if (carry)
							first.Value %= 10;

						// Move along.
						if (first != null)
							first = first.Next;
						if (second != null)
							second = second.Next;
					}
				}

				if (c.isNegative && c.IsZero())
					c.isNegative = false;

				return c;
			}
		}

		#endregion

		#region Subtraction

		public BigNumber Subtract(long value)
		{
			return Subtract(new BigNumber(value));
		}

		public BigNumber Subtract(BigNumber counter)
		{
			return Subtract(this, counter);
		}

		private static BigNumber Subtract(BigNumber a, BigNumber b)
		{
			if (b.IsZero())
				return a.Clone() as BigNumber;

			// There may be some recursive function calls here, but by the 
			// time the actual subtraction (else block) is performed, 
			// two criteria will be met:
			// - a and b will be positive 
			// - a will be greater than or equal to b

			if (a.isNegative && b.isNegative)
			{
				// -10 - (-11) = 1
				// -11 - (-10) = -1
				BigNumber aAbs = a.Abs(), bAbs = b.Abs();
				bool shouldNegate = aAbs > bAbs;

				BigNumber c = Subtract(aAbs, bAbs);
				c.isNegative = shouldNegate;
				return c;
			}
			else if (a.isNegative != b.isNegative)
			{
				// 10 - (-11) = 21
				// 11 - (-10) = 21
				// -10 - 11 =  -21
				// -11 - 10 =  -21
				BigNumber aAbs = a.Abs(), bAbs = b.Abs();
				bool shouldNegate = a.isNegative;

				BigNumber c = Add(aAbs, bAbs);
				c.isNegative = shouldNegate;
				return c;
			}
			else if (a < b)
			{
				BigNumber c = Subtract(b, a);
				c.isNegative = true;
				return c;
			}
			else
			{
				BigNumber c = a.Clone() as BigNumber;

				// a should be >= b at this point.
				LinkedListNode<byte> first, second;
				first = c.digits.First;
				second = b.digits.First;

				while (first != null || second != null)
				{
					if (first != null && second != null)
					{
						if (first.Value < second.Value)
						{
							first.Value += 10;

							// Borrowing logic.
							LinkedListNode<byte> borrowingVisiter = first.Next;
							while (true)
							{
								if (borrowingVisiter.Value == 0)
								{
									// Like adding 10 then subtracting 1 for the next borrow.
									borrowingVisiter.Value = 9;
									borrowingVisiter = borrowingVisiter.Next;
								}
								else
								{
									borrowingVisiter.Value--;
									break;
								}
							}
						}

						first.Value -= second.Value;
					}

					if (first != null)
						first = first.Next;
					if (second != null)
						second = second.Next;
				}

				c.TrimTailZeros();

				if (c.isNegative && c.IsZero())
					c.isNegative = false;

				return c;
			}
		}

		#endregion

		#region Multiplication

		public BigNumber Multiply(long value)
		{
			return Multiply(new BigNumber(value));
		}

		public BigNumber Multiply(BigNumber counter)
		{
			return Multiply(this, counter);
		}

		private static BigNumber Multiply(BigNumber a, BigNumber b)
		{
			// Shortcuts for simple operations.
			if (a.IsZero() || b.IsZero())
			{
				// Anything multiplied by 0 is 0.
				return new BigNumber();
			}
			else if (b.digits.Count == 1 && b.digits.First.Value == 1)
			{
				// Anything multiplied by 1 is itself.
				BigNumber c = a.Clone() as BigNumber;
				if (b.isNegative)
					c.isNegative = !c.isNegative;
				return c;
			}
			else
			{
				BigNumber c = new BigNumber();

				LinkedListNode<byte> second = b.digits.First;

				int rank = 0;
				while (second != null)
				{
					BigNumber additive = new BigNumber();

					for (int i = 0; i < rank; i++)
						additive.digits.AddLast(0);

					byte carry = 0;
					LinkedListNode<byte> first = a.digits.First;
					while (first != null || carry > 0)
					{
						if (first != null)
						{
							byte multiple = (byte)(first.Value * second.Value);

							if (carry > 0)
							{
								multiple += carry;
								carry = 0;
							}

							if (multiple >= 10)
							{
								carry = (byte)(multiple / 10);
								multiple %= 10;
							}

							additive.digits.AddLast(multiple);

							first = first.Next;
						}
						else if (carry > 0)
						{
							additive.digits.AddLast(carry);
							carry = 0;
						}
					}

					c += additive;

					rank++;
					second = second.Next;
				}

				c.isNegative = a.isNegative;

				if (b.isNegative)
					c.isNegative = !c.isNegative;

				return c;
			}
		}

		#endregion

		#region Division

		public BigNumber Divide(long value)
		{
			return Divide(new BigNumber(value));
		}

		public BigNumber Divide(BigNumber counter)
		{
			return Divide(this, counter);
		}

		private static BigNumber Divide(BigNumber a, BigNumber b)
		{
			if (b.IsZero())
			{
				throw new DivideByZeroException();
			}
			else if (b.digits.Count == 1 && b.digits.First.Value == 1)
			{
				// Anything divided by 1 is itself.
				BigNumber c = a.Clone() as BigNumber;
				if (b.isNegative)
					c.isNegative = !c.isNegative;
				return c;
			}
			else if (a.digits.Count < b.digits.Count)
			{
				// Decimal points are not supported so trying to divide a small 
				// number by a large number will always yield 0.
				return new BigNumber();
			}
			else
			{
				/*
					__100 <- (quotient)
				10 | 1000
				 ^		^- (a / dividend)
				 |
				(b / divisor)
				*/

				bool wasNegative = b.isNegative;
				b.isNegative = false;

				BigNumber quotient = new BigNumber();
				BigNumber dividend = new BigNumber();

				LinkedListNode<byte> second = a.digits.Last;
				while (second != null)
				{
					dividend.digits.AddFirst(second.Value);

					if (dividend < b)
					{
						quotient.digits.AddFirst(0);
					}
					else
					{
						byte quotientDigit = 0;
						while (true)
						{
							BigNumber divTmp = dividend - b;
							if (divTmp < 0)
								break;

							dividend = divTmp;
							quotientDigit++;
						}

						quotient.digits.AddFirst(quotientDigit);
					}

					second = second.Previous;
				}

				if (wasNegative)
					b.isNegative = true;

				BigNumber c = a.Clone() as BigNumber;
				c.digits = quotient.digits;

				if (b.isNegative)
					c.isNegative = !c.isNegative;

				c.TrimTailZeros();

				return c;
			}
		}

		#endregion

		#region Comparison Operators

		public bool Equals(BigNumber counter)
		{
			if (object.ReferenceEquals(counter, null))
				return false;

			return Equals(this, counter);
		}

		private bool Equals(BigNumber a, BigNumber b)
		{
			if (a.digits.Count == b.digits.Count)
			{
				LinkedListNode<byte> first = a.digits.Last, second = b.digits.Last;

				while (first != null && second != null)
				{
					if (first.Value != second.Value)
						return false;

					first = first.Previous;
					second = second.Previous;
				}

				return true;
			}
			return false;
		}

		public bool LessThan(BigNumber counter)
		{
			return LessThan(this, counter);
		}

		private bool LessThan(BigNumber a, BigNumber b)
		{
			if (a.isNegative && !b.isNegative)
				return true;
			else if (!a.isNegative && b.isNegative)
				return false;
			else if (a.isNegative && b.isNegative && a.digits.Count > b.digits.Count)
				return true;
			else if (a.isNegative && b.isNegative && a.digits.Count < b.digits.Count)
				return false;
			else if (!a.isNegative && !b.isNegative && a.digits.Count > b.digits.Count)
				return false;
			else if (!a.isNegative && !b.isNegative && a.digits.Count < b.digits.Count)
				return true;

			LinkedListNode<byte> first = a.digits.Last, second = b.digits.Last;

			while (first != null && second != null)
			{
				if (first.Value > second.Value)
					return false;
				if (first.Value < second.Value)
					return true;

				first = first.Previous;
				second = second.Previous;
			}

			return false;
		}

		public bool GreaterThan(BigNumber counter)
		{
			return GreaterThan(this, counter);
		}

		private bool GreaterThan(BigNumber a, BigNumber b)
		{
			if (a.isNegative && !b.isNegative)
				return false;
			else if (!a.isNegative && b.isNegative)
				return true;
			else if (a.isNegative && b.isNegative && a.digits.Count > b.digits.Count)
				return false;
			else if (a.isNegative && b.isNegative && a.digits.Count < b.digits.Count)
				return true;
			else if (!a.isNegative && !b.isNegative && a.digits.Count > b.digits.Count)
				return true;
			else if (!a.isNegative && !b.isNegative && a.digits.Count < b.digits.Count)
				return false;

			LinkedListNode<byte> first = a.digits.Last, second = b.digits.Last;

			while (first != null && second != null)
			{
				if (first.Value > second.Value)
					return true;

				first = first.Previous;
				second = second.Previous;
			}

			return false;
		}

		#endregion

		#region Implicit Operators

		public static implicit operator BigNumber(long l)
		{
			return new BigNumber(l);
		}

		#endregion

		#region Operator Overloads

		public static BigNumber operator ++(BigNumber a)
		{
			return a.Add(1);
		}

		public static BigNumber operator --(BigNumber a)
		{
			return a.Subtract(1);
		}

		public static BigNumber operator +(BigNumber a, BigNumber b)
		{
			return a.Add(b);
		}

		public static BigNumber operator -(BigNumber a, BigNumber b)
		{
			return a.Subtract(b);
		}

		public static BigNumber operator *(BigNumber a, BigNumber b)
		{
			return a.Multiply(b);
		}

		public static BigNumber operator /(BigNumber a, BigNumber b)
		{
			return a.Divide(b);
		}

		public static bool operator ==(BigNumber a, BigNumber b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(BigNumber a, BigNumber b)
		{
			return !a.Equals(b);
		}

		public static bool operator <(BigNumber a, BigNumber b)
		{
			return a.CompareTo(b) < 0;
		}

		public static bool operator >(BigNumber a, BigNumber b)
		{
			return a.CompareTo(b) > 0;
		}

		public static bool operator <=(BigNumber a, BigNumber b)
		{
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >=(BigNumber a, BigNumber b)
		{
			return a.CompareTo(b) >= 0;
		}

		#endregion

		#region Utils

		public BigNumber Abs()
		{
			BigNumber c = Clone() as BigNumber;
			c.isNegative = false;
			return c;
		}

		// This is to help with any negative flag for 0, and for trimming unused nodes.
		private bool IsZero()
		{
			foreach (byte digit in this.digits)
			{
				if (digit > 0)
					return false;
			}
			return true;
		}

		private void TrimTailZeros()
		{
			LinkedListNode<byte> tail = this.digits.Last;
			while (tail != null && tail.Value == 0)
			{
				tail = tail.Previous;
				this.digits.RemoveLast();
			}
		}

		private IEnumerable<byte> GetDigits(long value)
		{
			sbyte digit;
			for (int i = 0; ; i++)
			{
				digit = NthDigit(value, i);
				if (digit == -1)
					yield break;

				yield return (byte)digit;
			}
		}

		private sbyte NthDigit(long value, int n)
		{
			while (n-- > 0)
			{
				if (value < 10)
					return -1; // Less than n digits

				value /= 10;
			}
			return (sbyte)(value % 10);
		}

		#endregion

		public object Clone()
		{
			return new BigNumber()
			{
				digits = new LinkedList<byte>(this.digits),
				isNegative = this.isNegative
			};
		}

		public int CompareTo(BigNumber other)
		{
			if (this.Equals(other))
				return 0;
			else if (this.LessThan(other))
				return -1;
			else
				return 1;
		}

		public IEnumerator<byte> GetEnumerator()
		{
			LinkedListNode<byte> node = this.digits.Last;
			while (node != null)
			{
				yield return node.Value;
				node = node.Previous;
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as BigNumber);
		}

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (byte digit in this.digits)
				hash ^= digit;

			return hash;
		}

		public override string ToString()
		{
			return ToString(null, CultureInfo.CurrentCulture);
		}

		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this.digits.Count == 0)
				return "0";

			if (this.digits.Count == 1)
				return WithNegative(this.digits.First.Value.ToString());

			string formatted;
			switch (format)
			{
				case "n":
				case "N":
					formatted = FormatNumber();
					break;
				default:
					formatted = FormatDefault();
					break;
			}

			return WithNegative(formatted);
		}

		private string FormatNumber()
		{
			StringBuilder sb = new StringBuilder(this.digits.Count);

			LinkedListNode<byte> node = this.digits.First;
			for (int i = 0; node != null; i++)
			{
				if (i == 3)
				{
					sb.Insert(0, ',');
					i = 0;
				}

				sb.Insert(0, node.Value);

				node = node.Next;
			}

			return sb.ToString();
		}

		private string FormatDefault()
		{
			StringBuilder sb = new StringBuilder(this.digits.Count);

			LinkedListNode<byte> node = this.digits.Last;
			while (node != null)
			{
				sb.Append(node.Value);
				node = node.Previous;
			}

			return sb.ToString();
		}

		private string WithNegative(string number)
		{
			return string.Concat((this.isNegative ? "-" : string.Empty), number);
		}
	}
}