using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using org.Core.Collections;
using org.Core.Extensions;

namespace org.Core.Compression
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CompressionEntry : ICompressionEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public CompressionEntry()
        {
        }
        #endregion

        #region Constants
        const string                                DefaultOutputExtension          = "org";
        #endregion

        #region Fields
        string                                      _name                           = null;
        string                                      _fullName                       = null;
        string                                      _extension                      = null;

        bool                                        _requiresExtensionValidation    = false;

        readonly ListCollection<ICompressionEntry>  _innerRepository                = new ListCollection<ICompressionEntry>(true);
        readonly string[]                           _compressedExtensions           = new string[] { DefaultOutputExtension, "zip", "gz", "ids" };
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public bool HasEntries
        {
            get 
            {
                return (0 < _innerRepository.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICompressionEntryCollection Entries
        {
            get 
            {
                return new CompressionEntryCollection(_innerRepository);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanCompress
        {
            get 
            {
                if (!string.IsNullOrEmpty(FullName))
                {
                    //Checks if a file extension is the same as a compression extension.
                    if (Extension.ContainsAny(_compressedExtensions))
                    {
                        //Extracts a file extension from FullName to determine whether the extension matches one of compression extensions.                        
                        var _comparsion = RemoveDotFromExtension(Path.GetExtension(FullName));

                        if (!string.IsNullOrEmpty(_comparsion))
                        {
                            //If the file extension exists, the compression will not be performed. Otherwise, the compression will be performed.
                            return !_comparsion.ContainsAny(_compressedExtensions);
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanDecompress
        {
            get 
            {
                if (!string.IsNullOrEmpty(FullName))
                {
                    return !CanCompress;
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get 
            {
                return _name;
            }

            protected set
            {
                _name = value;

                if (!string.IsNullOrEmpty(_name) 
                    && !string.IsNullOrEmpty(_fullName))
                {
                    //Turns on the extension validation because Name is the same as FullName.
                    _requiresExtensionValidation = _name.Equals(_fullName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }

            protected set
            {
                _fullName = value;

                if (!string.IsNullOrEmpty(_fullName) 
                    && !string.IsNullOrEmpty(_name))
                {
                    //Turns on the extension validation because FullName is the same as Name.
                    _requiresExtensionValidation = _fullName.Equals(_name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(_extension))
                { 
                    //Extracts a file extension from EntryName.
                    _extension = RemoveDotFromExtension(Path.GetExtension(Name));

                    if (string.IsNullOrEmpty(_extension))
                    {
                        //If a file extension is empty or null, sets a default extension.
                        _extension = DefaultOutputExtension;
                    }

                    //Turns off the extension validation because a default extension has been set.
                    _requiresExtensionValidation = false;
                }
                else if (_requiresExtensionValidation)
                {
                    //Extracts a file extension from FullName to determine whether the extension equals another file extension.
                    var _comparsion = RemoveDotFromExtension(Path.GetExtension(FullName));

                    //If the file extension is the same as another one and it does not exist, sets a default extension.
                    if (_extension.Equals(_comparsion) && !_extension.ContainsAny(_compressedExtensions))
                    {
                        _extension = DefaultOutputExtension;
                    }

                    //Turns off the extension validation because it might be performed again.
                    _requiresExtensionValidation = false;
                }

                return _extension;
            }

            protected set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _extension = RemoveDotFromExtension(value);
                }
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        protected void BaseAddEntry(ICompressionEntry compressionEntry)
        {
            _innerRepository.Add(compressionEntry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        protected void RemoveEntry(ICompressionEntry compressionEntry)
        {
            _innerRepository.Remove(compressionEntry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        string RemoveDotFromExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                return extension.TrimStart('.');
            }

            return extension;
        }
        #endregion
    }
}
