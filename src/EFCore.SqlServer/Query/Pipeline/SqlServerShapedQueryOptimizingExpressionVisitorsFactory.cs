﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Query.Pipeline;
using Microsoft.EntityFrameworkCore.Relational.Query.Pipeline;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.Pipeline
{
    public class SqlServerShapedQueryOptimizingExpressionVisitorsFactory : RelationalShapedQueryOptimizingExpressionVisitorsFactory
    {
        private readonly IRelationalTypeMappingSource _relationalTypeMapping;
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public SqlServerShapedQueryOptimizingExpressionVisitorsFactory(
            IRelationalTypeMappingSource relationalTypeMapping,
            ISqlExpressionFactory sqlExpressionFactory)
        {
            _relationalTypeMapping = relationalTypeMapping;
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public override ShapedQueryOptimizingExpressionVisitors Create(QueryCompilationContext2 queryCompilationContext)
        {
            return new SqlServerShapedQueryOptimizingExpressionVisitors(queryCompilationContext, _relationalTypeMapping, _sqlExpressionFactory);
        }
    }
}