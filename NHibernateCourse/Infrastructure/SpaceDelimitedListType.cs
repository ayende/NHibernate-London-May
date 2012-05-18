using System;
using System.Collections.Generic;
using System.Data;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System.Linq;

namespace NHibernateCourse.Infrastructure
{
	public class SpaceDelimitedListType : IUserType
	{
		public new bool Equals(object x, object y)
		{
			return ((IList<string>)x).SequenceEqual((IList<string>)y);
		}

		public int GetHashCode(object x)
		{
			return ((IList<string>) x)
				.Aggregate(0, (current, s) => (current*397) ^ s.GetHashCode());
		}

		public object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			var val = rs[names[0]];
			if (val == DBNull.Value)
				return null;
			return ((string) val).Split(' ').ToList();
		}

		public void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			var p = (IDataParameter) cmd.Parameters[index];
			p.Value = string.Join(" ", ((IList<string>) value));
		}

		public object DeepCopy(object value)
		{
			return ((IList<string>) value).ToList();
		}

		public object Replace(object original, object target, object owner)
		{
			return ((IList<string>)original).ToList();
		}

		public object Assemble(object cached, object owner)
		{
			return cached;
		}

		public object Disassemble(object value)
		{
			return value;
		}

		public SqlType[] SqlTypes
		{
			get { return new SqlType[]{SqlTypeFactory.GetString(255), }; }
		}

		public Type ReturnedType
		{
			get { return typeof(IList<string>); }
		}

		public bool IsMutable
		{
			get { return true; }
		}
	}
}