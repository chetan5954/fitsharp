// Copyright � 2009 Syterra Software Inc.
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License version 2.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

using fitlibrary.table;
using fitSharp.Fit.Model;
using fitSharp.Machine.Engine;
using fitSharp.Machine.Model;

namespace fit.Operators {
    public class ComposeTable: ComposeOperator<Cell> {
        public bool CanCompose(Processor<Cell> processor, TypedValue instance) {
            return typeof(Table).IsAssignableFrom(instance.Type);
        }

        public Tree<Cell> Compose(Processor<Cell> processor, TypedValue instance) {
            return new Parse("td", string.Empty, MakeTable(processor, (Table)instance.Value), null);
        }

        private static Parse MakeTable(Processor<Cell> processor, Table theTable) {
            Parse rows = null;
            for (int row = theTable.Rows() - 1; row >= 0 ; row--) {
                Parse cells = null;
                for (int cell = theTable.Cells(row) - 1; cell >= 0 ; cell--) {
                    var newCell = (Parse) processor.Compose(theTable.StringAt(row, cell));
                    newCell.More = cells;
                    cells = newCell;
                }
                rows = new Parse("tr", string.Empty, cells, rows);
                
            }
            return new Parse("table", string.Empty,  rows, null);
        }
    }
}