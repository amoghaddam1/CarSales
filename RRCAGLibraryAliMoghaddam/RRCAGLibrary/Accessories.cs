/*
 * Name: Ali Moghaddam
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2019-09-12
 * Updated: 2019-09-16
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moghaddam.Ali.Business
{
    /// <summary>
    /// Enum to hold the chosen accessories.
    /// </summary>
    public enum Accessories
    {
        /// <summary>
        /// No accessory selected.
        /// </summary>
        None,

        /// <summary>
        /// Stereo system selected.
        /// </summary>
        StereoSystem,

        /// <summary>
        /// Leather interior selected.
        /// </summary>
        LeatherInterior,

        /// <summary>
        /// Stereo system and leather interior selected.
        /// </summary>
        StereoAndLeather,

        /// <summary>
        /// Computer navigation selected.
        /// </summary>
        CumputerNavigation,

        /// <summary>
        /// Stereo system and computer navigation selected.
        /// </summary>
        StereoandNavigation,

        /// <summary>
        /// Leather interior and computer navigation selected.
        /// </summary>
        LeatherAndNavigation,

        /// <summary>
        /// All accessories chosen.
        /// </summary>
        All
    }
}
