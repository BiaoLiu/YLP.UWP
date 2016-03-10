using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Triggers
{
    /// <summary>
    /// Implement this interface to be able to observe triggers or use them
    /// </summary>
    public interface ITriggerValue
    {
        /// <summary>
        /// Gets a value indicating whether this trigger is active.
        /// </summary>
        /// <value><c>true</c> if this trigger is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; }

        /// <summary>
        /// Occurs when the <see cref="IsActive"/> property has changed.
        /// </summary>
        event EventHandler IsActiveChanged;
    }
}
