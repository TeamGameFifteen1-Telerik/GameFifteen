namespace GameFifteen.Console.Styles
{
    using System.Collections.Generic;
    using System.Text;

    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Concrete decorator (Decorator design pattern).
    /// </summary>
    public class GridWithBorder : Decorator, IGameMember
    {
        private GridBorderStyle borderStyle;

        /// <summary>
        /// <see cref="GridWithBorder"/> constructor.
        /// </summary>
        /// <param name="gameMember">Game member to decorate</param>
        /// <param name="borderStyle">Border decoration</param>
        public GridWithBorder(IGameMember gameMember, GridBorderStyle borderStyle)
            : base(gameMember)
        {
            this.borderStyle = borderStyle;
        }

        public override string GetTextRepresentation()
        {
            var gridLines = base.GetTextRepresentation().Split('|');
            var gridWithBorder = new List<string>();
            var sb = new StringBuilder();

            gridWithBorder.Add(this.borderStyle.Top);
            for (int i = 0; i < gridLines.Length; i++)
            {
                sb.Append(this.borderStyle.Left)
                    .Append(gridLines[i])
                    .Append(this.borderStyle.Right);
                gridWithBorder.Add(sb.ToString());
                sb.Clear();
            }

            gridWithBorder.Add(this.borderStyle.Bottom);

            return string.Join("|", gridWithBorder);
        }
    }
}
