﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Relational.Query.Pipeline.SqlExpressions
{
    public class ExistsExpression : SqlExpression
    {
        public ExistsExpression(SelectExpression subquery, bool negated, RelationalTypeMapping typeMapping)
            : base(typeof(bool), typeMapping)
        {
            Subquery = subquery;
            Negated = negated;
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            var newSubquery = (SelectExpression)visitor.Visit(Subquery);

            return newSubquery != Subquery
                ? new ExistsExpression(newSubquery, Negated, TypeMapping)
                : this;
        }

        public SelectExpression Subquery { get; }
        public bool Negated { get; }

        public override bool Equals(object obj)
            => obj != null
            && (ReferenceEquals(this, obj)
                || obj is ExistsExpression existsExpression
                    && Equals(existsExpression));

        private bool Equals(ExistsExpression existsExpression)
            => base.Equals(existsExpression)
            && Subquery.Equals(existsExpression.Subquery)
            && Negated == existsExpression.Negated;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Subquery.GetHashCode();
                hashCode = (hashCode * 397) ^ Negated.GetHashCode();

                return hashCode;
            }
        }
    }
}