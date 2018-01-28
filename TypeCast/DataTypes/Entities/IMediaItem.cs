using System;
using TypeCast.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCast.DataTypes
{
    public interface IMediaItem
    {
        int MediaNodeId { get; set; }

        string Url { get; }

        string Name { get; }

        string Extension { get; }

        int Width { get; }

        int Height { get; }

        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        int FileSize { get; }

        MediaType Type { get; }
    }
}
