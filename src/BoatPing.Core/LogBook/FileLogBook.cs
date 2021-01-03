using System;
using System.Collections.Generic;
using System.IO;
using BoatPing.Core.Model;
using Yaapii.Atoms.IO;

namespace BoatPing.Core.LogBook
{
    /// <summary>
    /// A logbook that exists in ram.
    /// </summary>
    public sealed class FileLogBook : LogBookEnvelope
    {
        /// <summary>
        /// A logbook that exists in ram.
        /// </summary>
        public FileLogBook(string path) : base(() => new XiveLogBook(new FileHive(path, "logbook")))
        { }
    }
}
