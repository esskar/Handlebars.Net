﻿using System;
using System.Linq.Expressions;

namespace Handlebars.Compiler.Structure
{
    internal class BoolishExpression : HandlebarsExpression
    {
        public BoolishExpression(Expression condition)
        {
            ConditionExpression = condition;
        }

        public Expression ConditionExpression { get; }

        public override ExpressionType NodeType { get; } = (ExpressionType)HandlebarsExpressionType.BoolishExpression;

        public override Type Type => typeof(bool);
    }
}

