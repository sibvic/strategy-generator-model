﻿using ProfitRobots.StrategyGenerator.Model;
using ProfitRobots.StrategyGenerator.ModelParser.Implementation;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Parses entry order rate.
    /// </summary>
    public class EntryParser
    {
        StrategyModel model;

        public EntryParser(StrategyModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Parses condition from human-readable text.
        /// </summary>
        /// <param name="text">Text to parse</param>
        /// <exception cref="InvalidSemanticItemException">On unsupported value</exception>
        /// <exception cref="NotEnoughDataException">When there is not enought data to build the argument</exception>
        /// <exception cref="ParserException">On parsing error</exception>
        /// <returns>Parsed rate order</returns>
        public Order Parse(string text)
        {
            return new Order()
            {
                UserInput = text,
                ValueStack = ArgumentsBuilder.BuildArgs(text, model)
            };
        }

        private string ArgumentToString(FormulaItem arg)
        {
            switch (arg.ValueType)
            {
                case FormulaItem.VALUE:
                    return arg.Value;
                case FormulaItem.STREAM_VALUE:
                case FormulaItem.STREAM:
                    if (string.IsNullOrEmpty(arg.Substream))
                        return arg.Value;
                    return $"[{arg.Value}.{arg.Substream}]";
            }
            throw new ParserException(model, message: $"Failed to parse the input string");
        }
    }
}
