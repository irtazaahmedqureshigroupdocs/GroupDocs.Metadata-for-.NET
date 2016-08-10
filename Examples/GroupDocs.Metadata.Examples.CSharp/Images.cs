﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupDocs.Metadata.Formats.Image;
using GroupDocs.Metadata.Xmp.Schemas.DublinCore;
using GroupDocs.Metadata.Xmp;
using GroupDocs.Metadata.Standards.Exif;
using GroupDocs.Metadata.Standards.Exif.Jpeg;
using GroupDocs.Metadata.Examples.Utilities.CSharp;
using GroupDocs.Metadata.Tools;
using GroupDocs.Metadata.Standards.Xmp;
using GroupDocs.Metadata.Formats.AdobeApplication;
using GroupDocs.Metadata.Standards.Psd;
using GroupDocs.Metadata.Xmp.Types.Complex.Font;
using GroupDocs.Metadata.Xmp.Types.Complex.Dimensions;
using GroupDocs.Metadata.Xmp.Schemas.CameraRaw;
using GroupDocs.Metadata.Xmp.Types.Complex.Colorant;
using GroupDocs.Metadata.Xmp.Schemas.BasicJob;
using GroupDocs.Metadata.Xmp.Types.Complex.BasicJob;
using System.Drawing;
using System.IO;
using GroupDocs.Metadata.Xmp.Types.Complex.Thumbnail;
using GroupDocs.Metadata.Tools.Search;
using GroupDocs.Metadata.Formats.Cad;
using GroupDocs.Metadata.Standards.Cad;
using GroupDocs.Metadata.Tools.Comparison;
using GroupDocs.Metadata.Standards.Iptc;
using GroupDocs.Metadata.Xmp.Schemas.Iptc;
using GroupDocs.Metadata.Standards.Tiff;

namespace GroupDocs.Metadata.Examples.CSharp
{
    public static class Images
    {
        public static class JP2
        {
            // initialize file path
            //ExStart:SourceJP2FilePath
            private const string filePath = "Images/JP2/ExifSample.jp2";
            //ExEnd:SourceJP2FilePath

            #region working with XMP data
            /// <summary>
            ///Gets XMP properties from JP2 file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXmpPropertiesJP2Image
                    // initialize JP2Format
                    Jp2Format jp2Format = new Jp2Format(Common.MapSourceFilePath(filePath));

                    // get XMP data
                    XmpProperties xmpProperties = jp2Format.GetXmpProperties();

                    // show XMP data
                    foreach (string key in xmpProperties.Keys)
                    {
                        try
                        {
                            XmpNodeView xmpNodeView = xmpProperties[key];
                            Console.WriteLine("[{0}] = {1}", xmpNodeView.Name, xmpNodeView.Value);
                        }
                        catch { }
                    }
                    //ExEnd:GetXmpPropertiesJP2Image
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes XMP data of Jpeg2000 file and creates output file
            /// </summary> 
            public static void RemoveXMPData()
            {
                try
                {
                    //ExStart:RemoveXmpPropertiesJp2Image
                    // initialize JP2Format
                    Jp2Format jp2Format = new Jp2Format(Common.MapSourceFilePath(filePath));

                    // remove XMP package
                    jp2Format.RemoveXmpData();

                    // commit changes
                    jp2Format.Save(Common.MapDestinationFilePath(filePath));

                    //ExEnd:RemoveXmpPropertiesJp2Image
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP data of JP2 file and creates output file
            /// </summary> 
            public static void UpdateXMPProperties()
            {
                try
                {
                    //ExStart:UpdateXmpPropertiesJpegImage
                    // initialize JP2Format
                    Jp2Format jp2Format = new Jp2Format(Common.MapSourceFilePath(filePath));

                    // get xmp wrapper
                    XmpPacketWrapper xmpPacket = jp2Format.GetXmpData();

                    // create xmp wrapper if not exists
                    if (xmpPacket == null)
                    {
                        xmpPacket = new XmpPacketWrapper();
                    }

                    // check if DublinCore schema exists
                    if (!xmpPacket.ContainsPackage(Namespaces.DublinCore))
                    {
                        // if not - add DublinCore schema
                        xmpPacket.AddPackage(new DublinCorePackage());
                    }

                    // get DublinCore package
                    DublinCorePackage dublinCorePackage = (DublinCorePackage)xmpPacket.GetPackage(Namespaces.DublinCore);

                    string authorName = "New author";
                    string description = "New description";
                    string subject = "New subject";
                    string publisher = "New publisher";
                    string title = "New title";

                    // set author
                    dublinCorePackage.SetAuthor(authorName);
                    // set description
                    dublinCorePackage.SetDescription(description);
                    // set subject
                    dublinCorePackage.SetSubject(subject);
                    // set publisher
                    dublinCorePackage.SetPublisher(publisher);
                    // set title
                    dublinCorePackage.SetTitle(title);
                    // update XMP package
                    jp2Format.SetXmpData(xmpPacket);

                    // commit changes
                    jp2Format.Save(Common.MapDestinationFilePath(filePath));

                    //ExEnd:UpdateXmpPropertiesJP2Image
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Read Metadata of JP2 Format
            /// </summary> 
            public static void ReadMetadataJP2()
            {
                try
                {
                    //ExStart:ReadMetadataJP2
                    // initialize Jpeg2000 format
                    Jp2Format jp2Format = new Jp2Format((Common.MapSourceFilePath(filePath)));

                    // get height
                    int width = jp2Format.Width;

                    // get height
                    int height = jp2Format.Height;

                    // get comments
                    string[] comments = jp2Format.Comments;

                    foreach (var comm in comments)
                    {
                        Console.WriteLine("Comments: {0}", comm);
                    }
                    //ExEnd:ReadMetadataJP2
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            #endregion
        }

        public static class Jpeg
        {
            // initialize file path
            //ExStart:SourceJpegFilePath

            private const string filePath = "Images/Jpeg/ExifSample.jpeg";
            private const string barcodeFilePath = "Images/Jpeg/barcode.jpeg";

            //ExEnd:SourceJpegFilePath
            #region working with XMP data
            /// <summary>
            ///Gets XMP properties from Jpeg file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get XMP data
                    XmpProperties xmpProperties = jpegFormat.GetXmpProperties();

                    // show XMP data
                    foreach (string key in xmpProperties.Keys)
                    {
                        try
                        {
                            XmpNodeView xmpNodeView = xmpProperties[key];
                            Console.WriteLine("[{0}] = {1}", xmpNodeView.Name, xmpNodeView.Value);
                        }
                        catch { }
                    }
                    //ExEnd:GetXmpPropertiesJpegImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateXMPProperties()
            {
                try
                {
                    //ExStart:UpdateXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get xmp wrapper
                    XmpPacketWrapper xmpPacket = jpegFormat.GetXmpData();

                    // create xmp wrapper if not exists
                    if (xmpPacket == null)
                    {
                        xmpPacket = new XmpPacketWrapper();
                    }

                    // check if DublinCore schema exists
                    if (!xmpPacket.ContainsPackage(Namespaces.DublinCore))
                    {
                        // if not - add DublinCore schema
                        xmpPacket.AddPackage(new DublinCorePackage());
                    }

                    // get DublinCore package
                    DublinCorePackage dublinCorePackage = (DublinCorePackage)xmpPacket.GetPackage(Namespaces.DublinCore);

                    string authorName = "New author";
                    string description = "New description";
                    string subject = "New subject";
                    string publisher = "New publisher";
                    string title = "New title";

                    // set author
                    dublinCorePackage.SetAuthor(authorName);
                    // set description
                    dublinCorePackage.SetDescription(description);
                    // set subject
                    dublinCorePackage.SetSubject(subject);
                    // set publisher
                    dublinCorePackage.SetPublisher(publisher);
                    // set title
                    dublinCorePackage.SetTitle(title);
                    // update XMP package
                    jpegFormat.SetXmpData(xmpPacket);

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateXmpPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP values of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateXMPValues()
            {
                try
                {
                    //ExStart:UpdateXmpValuesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    const string dcFormat = "test format";
                    string[] dcContributors = { "test contributor" };
                    const string dcCoverage = "test coverage";
                    const string phCity = "NY";
                    const string phCountry = "USA";
                    const string xmpCreator = "GroupDocs.Metadata";

                    jpegFormat.XmpValues.Schemes.DublinCore.Format = dcFormat;
                    jpegFormat.XmpValues.Schemes.DublinCore.Contributors = dcContributors;
                    jpegFormat.XmpValues.Schemes.DublinCore.Coverage = dcCoverage;
                    jpegFormat.XmpValues.Schemes.Photoshop.City = phCity;
                    jpegFormat.XmpValues.Schemes.Photoshop.Country = phCountry;
                    jpegFormat.XmpValues.Schemes.XmpBasic.CreatorTool = xmpCreator;

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateXmpValuesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates PagedText XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void UpdatePagedTextXMPProperties()
            {
                try
                {
                    //ExStart:UpdatePagedTextXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get access to PagedText schema
                    var package = jpegFormat.XmpValues.Schemes.PagedText;


                    // update MaxPageSize
                    package.MaxPageSize = new Dimensions(600, 800);

                    // update number of pages
                    package.NumberOfPages = 10;

                    // update plate names
                    package.PlateNames = new string[] { "1", "2", "3" };

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdatePagedTextXmpPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates CameraRaw XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateCameraRawXMPProperties()
            {
                try
                {
                    //ExStart:UpdateCameraRawXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat JpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get access to CameraRaw schema
                    var package = JpegFormat.XmpValues.Schemes.CameraRaw;

                    // update properties
                    package.AutoBrightness = true;
                    package.AutoContrast = true;
                    package.CropUnits = CropUnits.Pixels;

                    // update white balance
                    package.SetWhiteBalance(WhiteBalance.Auto);

                    // commit changes
                    JpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateCameraRawXmpPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Basic Job XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateBasicJobXMPProperties()
            {
                try
                {
                    //ExStart:UpdateBasicJobTicketXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get xmp data
                    var xmp = jpegFormat.GetXmpData();

                    BasicJobTicketPackage package = null;

                    // looking for the BasicJob schema if xmp data is presented
                    if (xmp != null)
                    {
                        package = xmp.GetPackage(Namespaces.BasicJob) as BasicJobTicketPackage;
                    }
                    else
                    {
                        xmp = new XmpPacketWrapper();
                    }

                    if (package == null)
                    {
                        // create package if not exist
                        package = new BasicJobTicketPackage();

                        // and add it to xmp data
                        xmp.AddPackage(package);
                    }

                    // create array of jobs
                    Job[] jobs = new Job[1];
                    jobs[0] = new Job()
                    {
                        Id = "1",
                        Name = "test job"
                    };

                    // update schema
                    package.SetJobs(jobs);

                    // update xmp data
                    jpegFormat.SetXmpData(xmp);

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateBasicJobTicketXmpPropertiesJpegImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates thumbnails in XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateThumbnailInXMPData()
            {
                try
                {
                    //ExStart:UpdateThumbnailXmpPropertiesJpegImage

                    string path = Common.MapSourceFilePath(filePath);
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get image base64 string
                    string base64String;
                    using (Image image = Image.FromFile(path))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            base64String = Convert.ToBase64String(imageBytes);
                        }
                    }

                    // create image thumbnail
                    Thumbnail thumbnail = new Thumbnail { ImageBase64 = base64String };

                    // initialize array and add thumbnail
                    Thumbnail[] thumbnails = new Thumbnail[1];
                    thumbnails[0] = thumbnail;

                    // update thumbnails property in XMP Basic schema
                    jpegFormat.XmpValues.Schemes.XmpBasic.Thumbnails = thumbnails;

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));

                    //ExEnd:UpdateThumbnailXmpPropertiesJpegImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes XMP data of Jpeg file and creates output file
            /// </summary> 
            public static void RemoveXMPData()
            {
                try
                {
                    //ExStart:RemoveXmpPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // remove XMP package
                    jpegFormat.RemoveXmpData();
                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:RemoveXmpPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            #endregion

            #region working with Exif properties
            /// <summary>
            /// Gets Exif info from Jpeg file
            /// </summary> 
            public static void GetExifInfo()
            {
                try
                {
                    //ExStart:GetExifPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get EXIF data
                    JpegExifInfo exif = (JpegExifInfo)jpegFormat.GetExifInfo();

                    if (exif != null)
                    {
                        // get artist 
                        Console.WriteLine("Artist: {0}", exif.Artist);
                        // get description 
                        Console.WriteLine("Description: {0}", exif.ImageDescription);
                        // get user's comment 
                        Console.WriteLine("User Comment: {0}", exif.UserComment);
                        // get user's Model 
                        Console.WriteLine("Model: {0}", exif.Model);
                        // get user's Make 
                        Console.WriteLine("Make: {0}", exif.Make);
                        // get user's CameraOwnerName 
                        Console.WriteLine("CameraOwnerName: {0}", exif.CameraOwnerName);
                        // get longitude
                        Console.WriteLine("Longitude: {0}", exif.GPSData.Longitude[0].ToString());
                        // get latitude
                        Console.WriteLine("Latitude: {0}", exif.GPSData.Latitude[0].ToString());
                    }
                    //ExEnd:GetExifPropertiesJpegImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Exif info of Jpeg file and creates output file
            /// </summary> 
            public static void UpdateExifInfo()
            {
                try
                {
                    //ExStart:UpdateExifPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get EXIF data
                    JpegExifInfo exif = (JpegExifInfo)jpegFormat.GetExifInfo();
                    if (exif == null)
                    {
                        // initialize EXIF data if null
                        exif = new JpegExifInfo();
                    }

                    // set artist
                    exif.Artist = "Usman";
                    // set make
                    exif.Make = "ABC";
                    // set model
                    exif.Model = "S120";
                    // set the name of the camera's owner
                    exif.CameraOwnerName = "Owner";
                    // set description
                    exif.ImageDescription = "sample description";

                    // update EXIF data
                    jpegFormat.SetExifInfo(exif);

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateExifPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Exif info using properties and creates output file
            /// </summary> 
            public static void UpdateExifInfoUsingProperties()
            {
                try
                {
                    //ExStart:UpdateExifValuesUsingPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get EXIF data
                    JpegExifInfo exif = (JpegExifInfo)jpegFormat.ExifValues;

                    // set artist
                    exif.Artist = "new test artist";

                    // set the name of the camera's owner
                    exif.CameraOwnerName = "new camera owner's name";

                    // set description
                    exif.ImageDescription = "update test description";

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateExifValuesUsingPropertiesJpegImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes GPS Data of Jpeg file and creates output file
            /// </summary> 
            public static void RemoveGPSData()
            {
                try
                {
                    //ExStart:RemoveGPSDataJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // get location
                    GpsLocation location = jpegFormat.GetGpsLocation();
                    if (location != null)
                    {
                        // remove GPS location
                        jpegFormat.RemoveGpsLocation();
                    }

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:RemoveGPSDataJpegImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes Exif info of Jpeg file and creates output file
            /// </summary> 
            public static void RemoveExifInfo()
            {
                try
                {
                    //ExStart:RemoveExifPropertiesJpegImage
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // remove Exif info
                    jpegFormat.RemoveExifInfo();

                    // commit changes
                    jpegFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:RemoveExifPropertiesJpegImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            #endregion

            #region Working with IPTC Metadata
            /// <summary>
            /// Gets IPTC metadata from Jpeg file
            /// </summary> 
            public static void GetIPTCMetadata()
            {
                try
                {
                    //ExStart:GetIPTCMetadata
                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // if file contains iptc metadata
                    if (jpegFormat.HasIptc)
                    {
                        // get iptc collection
                        IptcCollection iptcCollection = jpegFormat.GetIptc();

                        // go through array and write property name and formatted value
                        foreach (IptcProperty iptcProperty in iptcCollection)
                        {
                            Console.WriteLine(string.Format("{0}: {1}", iptcProperty.Name, iptcProperty.GetFormattedValue()));
                        }

                        // initialize IptcDataSetCollection to read well-known properties
                        IptcDataSetCollection dsCollection = new IptcDataSetCollection(iptcCollection);

                        // try to read Application Record dataset
                        if (dsCollection.ApplicationRecord != null)
                        {
                            // get category
                            string category = dsCollection.ApplicationRecord.Category;

                            // get headline
                            string headline = dsCollection.ApplicationRecord.Headline;
                        }

                        if (dsCollection.EnvelopeRecord != null)
                        {
                            // get model version
                            int? modelVersion = dsCollection.EnvelopeRecord.ModelVersion;

                            // get dataSent property
                            DateTime? dataSent = dsCollection.EnvelopeRecord.DataSent;
                        }
                    }
                    //ExEnd:GetIPTCMetadata
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Gets IPTC metadata from XMP in Jpeg file
            /// </summary>
            public static void GetIPTCPhotoMetadataFromXMP()
            {
                try
                {
                    //ExStart:GetIPTCPhotoMetadataFromXMP
                    // get xmp metadata
                    XmpPacketWrapper xmpWrapper = MetadataUtility.ExtractXmpPackage(Common.MapSourceFilePath(filePath));

                    if (xmpWrapper == null)
                    {
                        xmpWrapper = new XmpPacketWrapper();
                    }

                    // add iptc4xmpcore if not exist
                    if (!xmpWrapper.ContainsPackage(Namespaces.Iptc4XmpCore))
                    {
                        xmpWrapper.AddPackage(new IptcCorePackage());
                    }

                    // get iptc4XmpCore package
                    IptcCorePackage iptcCorePackage = (IptcCorePackage)xmpWrapper.GetPackage(Namespaces.Iptc4XmpCore);

                    Console.WriteLine("Country Code: {0}", iptcCorePackage.CountryCode);
                    Console.WriteLine("Sub Location: {0}", iptcCorePackage.Sublocation);
                    Console.WriteLine("Intellectual Genre: {0}", iptcCorePackage.IntellectualGenre);
                    //ExEnd:GetIPTCPhotoMetadataFromXMP
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates IPTC metadata in XMP in Jpeg file
            /// </summary>
            public static void UpdateIPTCPhotoMetadataFromXMP()
            {
                try
                {
                    //ExStart:UpdateIPTCPhotoMetadataFromXMP
                    // get xmp metadata
                    XmpPacketWrapper xmpWrapper = MetadataUtility.ExtractXmpPackage(Common.MapSourceFilePath(filePath));

                    if (xmpWrapper == null)
                    {
                        xmpWrapper = new XmpPacketWrapper();
                    }

                    // add iptc4xmpcore if not exist
                    if (!xmpWrapper.ContainsPackage(Namespaces.Iptc4XmpCore))
                    {
                        xmpWrapper.AddPackage(new IptcCorePackage());
                    }

                    // get iptc4XmpCore package
                    IptcCorePackage iptcCorePackage = (IptcCorePackage)xmpWrapper.GetPackage(Namespaces.Iptc4XmpCore);

                    // set country code
                    iptcCorePackage.CountryCode = "new country code";

                    // set sublocation
                    iptcCorePackage.Sublocation = "new sublocation";

                    // update intellectual genre
                    iptcCorePackage.IntellectualGenre = "music";

                    // save changes to another file
                    MetadataUtility.UpdateMetadata(Common.MapSourceFilePath(filePath), new XmpMetadata(xmpWrapper), Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateIPTCPhotoMetadataFromXMP
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates IPTC metadata of Jpeg file
            /// </summary>
            public static void UpdateIPTCMetadataOfJPEG()
            {
                try
                {
                    //ExStart:UpdateIPTCMetadataOfJPEG

                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // initialize IptcCollection
                    IptcCollection collection = new IptcCollection();

                    // add string property
                    collection.Add(new IptcProperty(2, "category", 15, "formats"));

                    // add integer property
                    collection.Add(new IptcProperty(2, "urgency", 10, 5));

                    // update iptc metadata
                    jpegFormat.UpdateIptc(collection);

                    // and commit changes
                    jpegFormat.Save();
                    //ExEnd:UpdateIPTCPhotoMetadataFromXMP
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Remove IPTC metadata of Jpeg file
            /// </summary>
            public static void RemoveIPTCMetadataOfJPEG()
            {
                try
                {
                    //ExStart:RemoveIPTCMetadataOfJPEG

                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // remove iptc
                    jpegFormat.RemoveIptc();

                    // and commit changes
                    jpegFormat.Save();
                    //ExEnd:RemoveIPTCMetadataOfJPEG
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            ///Update ApplicationRecord/EnvelopeRecord datasets of IPTC metadata
            /// </summary>
            public static void UpdateIPTCMetadataOfApplicationRecord()
            {
                try
                {
                    //ExStart:UpdateIPTCMetadataOfApplicationRecord

                    // initialize JpegFormat
                    JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(filePath));

                    // initialize dataset
                    IptcApplicationRecord applicationRecord = new IptcApplicationRecord();

                    // update category
                    applicationRecord.Category = "category";

                    // update copyright notice
                    applicationRecord.CopyrightNotice = "Aspose";

                    // update release date
                    applicationRecord.ReleaseDate = DateTime.Now;

                    // update iptc metadata
                    jpegFormat.UpdateIptc(applicationRecord);

                    // and commit changes
                    jpegFormat.Save();

                    //EXEnd:UpdateIPTCMetadataOfApplicationRecord
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            #endregion

            /// <summary>
            /// Detects barcodes in the Jpeg
            /// </summary>
            public static void DetectBarcodeinJpeg()
            {
                //ExStart:DetectBarcodeinJpeg
                // initialize JpegFormat
                JpegFormat jpegFormat = new JpegFormat(Common.MapSourceFilePath(barcodeFilePath));

                // get barcodes:  UPCA, UPCE, EAN13
                string[] barCodes = jpegFormat.GetBarCodeTypes();

                Console.WriteLine("Barcode Detected:\n");

                for (int i = 0; i < barCodes.Length; i++)
                {
                    Console.WriteLine("Code Type: {0}", barCodes[i].ToString());
                }

                //ExEnd:DetectBarcodeinJpeg
            }
        }

        public static class Gif
        {
            // initialize file path
            //ExStart:SourceGifFilePath
            private const string filePath = "Images/Gif/sample.gif";
            //ExEnd:SourceGifFilePath
            /// <summary>
            ///Gets XMP properties of Gif file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXMPPropertiesGifImage

                    // initialize GifFormat
                    GifFormat gifFormat = new GifFormat(Common.MapSourceFilePath(filePath));
                    if (gifFormat.IsSupportedXmp)
                    {
                        // get XMP data
                        XmpProperties xmpProperties = gifFormat.GetXmpProperties();

                        // show XMP data
                        foreach (string key in xmpProperties.Keys)
                        {
                            XmpNodeView xmpNodeView = xmpProperties[key];
                            Console.WriteLine("[{0}] = {1}", xmpNodeView.Name, xmpNodeView.Value);
                        }
                    }
                    //ExEnd:GetXMPPropertiesGifImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP data of Gif file and creates output file
            /// </summary> 
            public static void UpdateXMPProperties()
            {
                try
                {
                    //ExStart:UpdateXMPPropertiesGifImage
                    // initialize GifFormat
                    GifFormat gifFormat = new GifFormat(Common.MapSourceFilePath(filePath));
                    if (gifFormat.IsSupportedXmp)
                    {
                        // get xmp wrapper
                        XmpPacketWrapper xmpPacket = gifFormat.GetXmpData();

                        // create xmp wrapper if not exists
                        if (xmpPacket == null)
                        {
                            xmpPacket = new XmpPacketWrapper();
                        }

                        // check if DublinCore schema exists
                        if (!xmpPacket.ContainsPackage(Namespaces.DublinCore))
                        {
                            // if not - add DublinCore schema
                            xmpPacket.AddPackage(new DublinCorePackage());
                        }

                        // get DublinCore package
                        DublinCorePackage dublinCorePackage = (DublinCorePackage)xmpPacket.GetPackage(Namespaces.DublinCore);

                        string authorName = "New author";
                        string description = "New description";
                        string subject = "New subject";
                        string publisher = "New publisher";
                        string title = "New title";

                        // set author
                        dublinCorePackage.SetAuthor(authorName);
                        // set description
                        dublinCorePackage.SetDescription(description);
                        // set subject
                        dublinCorePackage.SetSubject(subject);
                        // set publisher
                        dublinCorePackage.SetPublisher(publisher);
                        // set title
                        dublinCorePackage.SetTitle(title);
                        // update XMP package
                        gifFormat.SetXmpData(xmpPacket);

                        // commit changes
                        gifFormat.Save(Common.MapDestinationFilePath(filePath));
                    }
                    //ExEnd:UpdateXMPPropertiesGifImage
                    Console.WriteLine("File saved in destination folder.");

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP values of Gif file and creates output file
            /// </summary> 
            public static void UpdateXMPValues()
            {
                try
                {
                    //ExStart:UpdateXmpValuesGifImage
                    // initialize GifFormat
                    GifFormat GifFormat = new GifFormat(Common.MapSourceFilePath(filePath));

                    const string dcFormat = "test format";
                    string[] dcContributors = { "test contributor" };
                    const string dcCoverage = "test coverage";
                    const string phCity = "NY";
                    const string phCountry = "USA";
                    const string xmpCreator = "GroupDocs.Metadata";

                    GifFormat.XmpValues.Schemes.DublinCore.Format = dcFormat;
                    GifFormat.XmpValues.Schemes.DublinCore.Contributors = dcContributors;
                    GifFormat.XmpValues.Schemes.DublinCore.Coverage = dcCoverage;
                    GifFormat.XmpValues.Schemes.Photoshop.City = phCity;
                    GifFormat.XmpValues.Schemes.Photoshop.Country = phCountry;
                    GifFormat.XmpValues.Schemes.XmpBasic.CreatorTool = xmpCreator;

                    // commit changes
                    GifFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateXmpValuesGifImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates PagedText XMP data of Gif file and creates output file
            /// </summary> 
            public static void UpdatePagedTextXMPProperties()
            {
                try
                {
                    //ExStart:UpdatePagedTextXmpPropertiesGifImage
                    // initialize GifFormat
                    GifFormat GifFormat = new GifFormat(Common.MapSourceFilePath(filePath));

                    // get access to PagedText schema
                    var package = GifFormat.XmpValues.Schemes.PagedText;

                    // update MaxPageSize
                    package.MaxPageSize = new Dimensions(600, 800);

                    // update number of pages
                    package.NumberOfPages = 10;

                    // update plate names
                    package.PlateNames = new string[] { "1", "2", "3" };

                    // commit changes
                    GifFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdatePagedTextXmpPropertiesGifImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates CameraRaw XMP data of Gif file and creates output file
            /// </summary> 
            public static void UpdateCameraRawXMPProperties()
            {
                try
                {
                    //ExStart:UpdateCameraRawXmpPropertiesGifImage
                    // initialize GifFormat
                    GifFormat GifFormat = new GifFormat(Common.MapSourceFilePath(filePath));

                    // get access to CameraRaw schema
                    var package = GifFormat.XmpValues.Schemes.CameraRaw;

                    // update properties
                    package.AutoBrightness = true;
                    package.AutoContrast = true;
                    package.CropUnits = CropUnits.Pixels;

                    // update white balance
                    package.SetWhiteBalance(WhiteBalance.Auto);

                    // commit changes
                    GifFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateCameraRawXmpPropertiesGifImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Basic Job XMP data of Gif file and creates output file
            /// </summary> 
            public static void UpdateBasicJobXMPProperties()
            {
                try
                {
                    //ExStart:UpdateBasicJobTicketXmpPropertiesGifImage
                    // initialize GifFormat
                    GifFormat gifFormat = new GifFormat(Common.MapSourceFilePath(filePath));

                    // get xmp data
                    var xmp = gifFormat.GetXmpData();

                    BasicJobTicketPackage package = null;

                    // looking for the BasicJob schema if xmp data is presented
                    if (xmp != null)
                    {
                        package = xmp.GetPackage(Namespaces.BasicJob) as BasicJobTicketPackage;
                    }
                    else
                    {
                        xmp = new XmpPacketWrapper();
                    }

                    if (package == null)
                    {
                        // create package if not exist
                        package = new BasicJobTicketPackage();

                        // and add it to xmp data
                        xmp.AddPackage(package);
                    }

                    // create array of jobs
                    Job[] jobs = new Job[1];
                    jobs[0] = new Job()
                    {
                        Id = "1",
                        Name = "test job"
                    };

                    // update schema
                    package.SetJobs(jobs);

                    // update xmp data
                    gifFormat.SetXmpData(xmp);

                    // commit changes
                    gifFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateBasicJobTicketXmpPropertiesGifImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates thumbnails in XMP data of Gif file and creates output file
            /// </summary> 
            public static void UpdateThumbnailInXMPData()
            {
                try
                {
                    //ExStart:UpdateThumbnailXmpPropertiesGifImage

                    string path = Common.MapSourceFilePath(filePath);
                    // initialize GifFormat
                    GifFormat gifFormat = new GifFormat(Common.MapSourceFilePath(filePath));

                    // get image base64 string
                    string base64String;
                    using (Image image = Image.FromFile(path))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            base64String = Convert.ToBase64String(imageBytes);
                        }
                    }

                    // create image thumbnail
                    Thumbnail thumbnail = new Thumbnail { ImageBase64 = base64String };

                    // initialize array and add thumbnail
                    Thumbnail[] thumbnails = new Thumbnail[1];
                    thumbnails[0] = thumbnail;

                    // update thumbnails property in XMP Basic schema
                    gifFormat.XmpValues.Schemes.XmpBasic.Thumbnails = thumbnails;

                    // commit changes
                    gifFormat.Save(Common.MapDestinationFilePath(filePath));

                    //ExEnd:UpdateThumbnailXmpPropertiesGifImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes XMP data of Gif file and creates output file
            /// </summary> 
            public static void RemoveXMPProperties()
            {
                try
                {
                    //ExStart:RemoveXMPPropertiesGifImage 
                    // initialize GifFormat
                    GifFormat gifFormat = new GifFormat(Common.MapSourceFilePath(filePath));
                    if (gifFormat.IsSupportedXmp)
                    {
                        // remove XMP package
                        gifFormat.RemoveXmpData();

                        // commit changes
                        gifFormat.Save(Common.MapDestinationFilePath(filePath));
                    }
                    //ExEnd:RemoveXMPPropertiesGifImage 
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

        }

        public static class Png
        {
            // initialize file path
            //ExStart:SourcePngFilePath
            private const string filePath = "Images/Png/sample2.png";
            //ExEnd:SourcePngFilePath
            /// <summary>
            ///Gets XMP properties from Png file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXMPPropertiesPngImage 
                    // initialize PngFormat
                    PngFormat pngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get XMP data
                    XmpProperties xmpProperties = pngFormat.GetXmpProperties();
                    if (xmpProperties != null)
                    {
                        // show XMP data
                        foreach (string key in xmpProperties.Keys)
                        {
                            XmpNodeView xmpNodeView = xmpProperties[key];
                            Console.WriteLine("[{0}] = {1}", xmpNodeView.Name, xmpNodeView.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No XMP data found.");
                    }
                    //ExEnd:GetXMPPropertiesPngImage 
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP data of Png file and creates output file
            /// </summary> 
            public static void UpdateXMPData()
            {
                try
                {
                    //ExStart:UpdateXMPPropertiesPngImage 
                    // initialize PngFormat
                    PngFormat pngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get xmp wrapper
                    XmpPacketWrapper xmpPacket = pngFormat.GetXmpData();

                    // create xmp wrapper if not exists
                    if (xmpPacket == null)
                    {
                        xmpPacket = new XmpPacketWrapper();
                    }

                    // check if DublinCore schema exists
                    if (!xmpPacket.ContainsPackage(Namespaces.DublinCore))
                    {
                        // if not - add DublinCore schema
                        xmpPacket.AddPackage(new DublinCorePackage());
                    }

                    // get DublinCore package
                    DublinCorePackage dublinCorePackage = (DublinCorePackage)xmpPacket.GetPackage(Namespaces.DublinCore);

                    string authorName = "New author";
                    string description = "New description";
                    string subject = "New subject";
                    string publisher = "New publisher";
                    string title = "New title";
                    // set author
                    dublinCorePackage.SetAuthor(authorName);
                    // set description
                    dublinCorePackage.SetDescription(description);
                    // set subject
                    dublinCorePackage.SetSubject(subject);
                    // set publisher
                    dublinCorePackage.SetPublisher(publisher);
                    // set title
                    dublinCorePackage.SetTitle(title);
                    // update XMP package
                    pngFormat.SetXmpData(xmpPacket);

                    // commit changes
                    pngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateXMPPropertiesPngImage 
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates XMP values of Png file and creates output file
            /// </summary> 
            public static void UpdateXMPValues()
            {
                try
                {
                    //ExStart:UpdateXmpValuesPngImage
                    // initialize PngFormat
                    PngFormat PngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    const string dcFormat = "test format";
                    string[] dcContributors = { "test contributor" };
                    const string dcCoverage = "test coverage";
                    const string phCity = "NY";
                    const string phCountry = "USA";
                    const string xmpCreator = "GroupDocs.Metadata";

                    PngFormat.XmpValues.Schemes.DublinCore.Format = dcFormat;
                    PngFormat.XmpValues.Schemes.DublinCore.Contributors = dcContributors;
                    PngFormat.XmpValues.Schemes.DublinCore.Coverage = dcCoverage;
                    PngFormat.XmpValues.Schemes.Photoshop.City = phCity;
                    PngFormat.XmpValues.Schemes.Photoshop.Country = phCountry;
                    PngFormat.XmpValues.Schemes.XmpBasic.CreatorTool = xmpCreator;

                    // commit changes
                    PngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateXmpValuesPngImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates PagedText XMP data of Png file and creates output file
            /// </summary> 
            public static void UpdatePagedTextXMPProperties()
            {
                try
                {
                    //ExStart:UpdatePagedTextXmpPropertiesPngImage
                    // initialize PngFormat
                    PngFormat PngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get access to PagedText schema
                    var package = PngFormat.XmpValues.Schemes.PagedText;

                    // update MaxPageSize
                    package.MaxPageSize = new Dimensions(600, 800);

                    // update number of pages
                    package.NumberOfPages = 10;

                    // update plate names
                    package.PlateNames = new string[] { "1", "2", "3" };

                    // commit changes
                    PngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdatePagedTextXmpPropertiesPngImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates CameraRaw XMP data of Png file and creates output file
            /// </summary> 
            public static void UpdateCameraRawXMPProperties()
            {
                try
                {
                    //ExStart:UpdateCameraRawXmpPropertiesPngImage
                    // initialize PngFormat
                    PngFormat PngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get access to CameraRaw schema
                    var package = PngFormat.XmpValues.Schemes.CameraRaw;

                    // update properties
                    package.AutoBrightness = true;
                    package.AutoContrast = true;
                    package.CropUnits = CropUnits.Pixels;

                    // update white balance
                    package.SetWhiteBalance(WhiteBalance.Auto);

                    // commit changes
                    PngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateCameraRawXmpPropertiesPngImage
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Basic Job XMP data of Png file and creates output file
            /// </summary> 
            public static void UpdateBasicJobXMPProperties()
            {
                try
                {
                    //ExStart:UpdateBasicJobTicketXmpPropertiesPngImage
                    // initialize PngFormat
                    PngFormat pngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get xmp data
                    var xmp = pngFormat.GetXmpData();

                    BasicJobTicketPackage package = null;

                    // looking for the BasicJob schema if xmp data is presented
                    if (xmp != null)
                    {
                        package = xmp.GetPackage(Namespaces.BasicJob) as BasicJobTicketPackage;
                    }
                    else
                    {
                        xmp = new XmpPacketWrapper();
                    }

                    if (package == null)
                    {
                        // create package if not exist
                        package = new BasicJobTicketPackage();

                        // and add it to xmp data
                        xmp.AddPackage(package);
                    }

                    // create array of jobs
                    Job[] jobs = new Job[1];
                    jobs[0] = new Job()
                    {
                        Id = "1",
                        Name = "test job"
                    };

                    // update schema
                    package.SetJobs(jobs);

                    // update xmp data
                    pngFormat.SetXmpData(xmp);

                    // commit changes
                    pngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateBasicJobTicketXmpPropertiesPngImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates thumbnails in XMP data of Png file and creates output file
            /// </summary> 
            public static void UpdateThumbnailInXMPData()
            {
                try
                {
                    //ExStart:UpdateThumbnailXmpPropertiesPngImage

                    string path = Common.MapSourceFilePath(filePath);
                    // initialize PngFormat
                    PngFormat pngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // get image base64 string
                    string base64String;
                    using (Image image = Image.FromFile(path))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            base64String = Convert.ToBase64String(imageBytes);
                        }
                    }

                    // create image thumbnail
                    Thumbnail thumbnail = new Thumbnail { ImageBase64 = base64String };

                    // initialize array and add thumbnail
                    Thumbnail[] thumbnails = new Thumbnail[1];
                    thumbnails[0] = thumbnail;

                    // update thumbnails property in XMP Basic schema
                    pngFormat.XmpValues.Schemes.XmpBasic.Thumbnails = thumbnails;

                    // commit changes
                    pngFormat.Save(Common.MapDestinationFilePath(filePath));

                    //ExEnd:UpdateThumbnailXmpPropertiesPngImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes XMP data of Png file and creates output file
            /// </summary> 
            public static void RemoveXMPData()
            {
                try
                {
                    //ExStart:RemoveXMPPropertiesPngImage 
                    // initialize PngFormat
                    PngFormat pngFormat = new PngFormat(Common.MapSourceFilePath(filePath));

                    // remove XMP package
                    pngFormat.RemoveXmpData();

                    // commit changes
                    pngFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:RemoveXMPPropertiesPngImage 
                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }

        public static class Tiff
        {
            // initialize file path
            //ExStart:SourceTiffFilePath
            private const string filePath = "Images/Tiff/sample.tif";
            //ExEnd:SourceTiffFilePath

            /// <summary>
            /// Gets Exif info from Tiff file
            /// </summary> 
            public static void GetExifInfo()
            {
                try
                {
                    //ExStart:GetExifPropertiesTiffImage
                    // initialize TiffFormat
                    TiffFormat tiffFormat = new TiffFormat(Common.MapSourceFilePath(filePath));

                    // get EXIF data
                    ExifInfo exif = tiffFormat.GetExifInfo();

                    if (exif != null)
                    {
                        // get BodySerialNumber 
                        Console.WriteLine("Body Serial Number: {0}", exif.BodySerialNumber);
                        // get CameraOwnerName 
                        Console.WriteLine("Camera Owner Name: {0}", exif.CameraOwnerName);
                        // get CFAPattern 
                        Console.WriteLine("CFA Pattern: {0}", exif.CFAPattern);
                        // get GPSData 
                        Console.WriteLine("GPS Data: {0}", exif.GPSData);
                        // get UserComment 
                        Console.WriteLine("User Comment: {0}", exif.UserComment);
                    }
                    //ExEnd:GetExifPropertiesTiffImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Exif info of Tiff file and creates output file
            /// </summary> 
            public static void UpdateExifInfo()
            {
                try
                {
                    //ExStart:UpdateExifPropertiesTiffImage
                    // initialize TiffFormat
                    TiffFormat tiffFormat = new TiffFormat(Common.MapSourceFilePath(filePath));

                    // get EXIF data
                    ExifInfo exif = tiffFormat.GetExifInfo();

                    exif.UserComment = "New User Comment";
                    exif.BodySerialNumber = "New Body Serial Number";
                    exif.CameraOwnerName = "New Camera Owner Name";

                    // update EXIF info
                    tiffFormat.UpdateExifInfo(exif);

                    // commit changes and save output file
                    tiffFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateExifPropertiesTiffImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Updates Exif info using properties and creates output file
            /// </summary> 
            public static void UpdateExifInfoUsingProperties()
            {
                try
                {
                    //ExStart:UpdateExifValuesUsingPropertiesTiffImage
                    // initialize TiffFormat
                    TiffFormat tiffFormat = new TiffFormat(Common.MapSourceFilePath(filePath));

                    tiffFormat.ExifValues.CameraOwnerName = "camera owner's name";

                    // set user comment
                    tiffFormat.ExifValues.UserComment = "user's comment";

                    // commit changes
                    tiffFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:UpdateExifValuesUsingPropertiesTiffImage

                    Console.WriteLine("File saved in destination folder.");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Removes Exif info from Tiff file
            /// </summary> 
            public static void RemoveExifInfo()
            {
                try
                {
                    //ExStart:RemoveExifPropertiesTiffImage
                    // initialize TiffFormat
                    TiffFormat tiffFormat = new TiffFormat(Common.MapSourceFilePath(filePath));

                    // remove Exif info
                    tiffFormat.RemoveExifInfo();

                    // commit changes and save output file
                    tiffFormat.Save(Common.MapDestinationFilePath(filePath));
                    //ExEnd:RemoveExifPropertiesTiffImage
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            /// <summary>
            ///Gets XMP properties from Tiff file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXMPPropertiesPngImage 
                    // initialize PngFormat
                    TiffFormat tiff = new TiffFormat(Common.MapSourceFilePath(filePath));

                    // get xmp
                    XmpPacketWrapper xmpPacket = tiff.GetXmpData();
                    if (xmpPacket != null)
                    {
                        // show XMP data
                        Console.WriteLine("XMP Data Found");
                    }
                    else
                    {
                        Console.WriteLine("No XMP data found.");
                    }
                    //ExEnd:GetXMPPropertiesPngImage 
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            /// <summary>
            ///ReadTiff File Directory Tags from Tiff file
            /// </summary>
            public static void ReadTiffFileDirectoryTags()
            {
                //ExStart:ReadTiffFileDirectoryTags 
                // initialize PngFormat
                TiffFormat tiffFormat = new TiffFormat(Common.MapSourceFilePath(filePath));

                // get IFD
                TiffIfd[] directories = tiffFormat.ImageFileDirectories;

                if (directories.Length > 0)
                {
                    // get tags of the first IFD
                    TiffTag[] tags = tiffFormat.GetTags(directories[0]);

                    // write tags to the console
                    foreach (TiffTag tiffTag in tags)
                    {
                        Console.WriteLine("tag: {0}", tiffTag.DefinedTag);
                        switch (tiffTag.TagType)
                        {
                            case TiffTagType.Ascii:
                                TiffAsciiTag asciiTag = tiffTag as TiffAsciiTag;
                                Console.WriteLine("Value: {0}", asciiTag.Value);
                                break;

                            case TiffTagType.Short:
                                TiffShortTag shortTag = tiffTag as TiffShortTag;
                                Console.WriteLine("Value: {0}", shortTag.Value);
                                break;

                            default:
                                break;
                        }
                    }
                }
                //ExEnd:ReadTiffFileDirectoryTags
            }

        }

        public static class Psd
        {
            // initialize file path
            //ExStart:SourcePSDFilePath
            private const string filePath = "Images/Psd/sample.psd";
            //ExEnd:SourcePSDFilePath

            /// <summary>
            /// Gets Psd Info
            /// </summary> 
            public static void GetPsdInfo()
            {
                try
                {
                    //ExStart:GetPsdInfo
                    // initialize PsdFormat 
                    PsdFormat psdFormat = new PsdFormat(Common.MapSourceFilePath(filePath));

                    // get PSD info
                    PsdMetadata metadata = psdFormat.GetPsdInfo();

                    if (metadata != null)
                    {
                        // get ChannelsCount 
                        Console.WriteLine("Channels Count: {0}", metadata.ChannelsCount);
                        // get ColorMode 
                        Console.WriteLine("Color Mode: {0}", metadata.ColorMode);
                        // get CompressionMethod 
                        Console.WriteLine("Compression Method: {0}", metadata.CompressionMethod);
                        // get Height 
                        Console.WriteLine("Height: {0}", metadata.Height);
                        // get Width 
                        Console.WriteLine("Width: {0}", metadata.Width);
                        // get PhotoshopVersion 
                        Console.WriteLine("Photoshop Version: {0}", metadata.PhotoshopVersion);

                    }
                    //ExEnd:GetPsdInfo
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            /// <summary>
            /// Gets XMP Properies in PSD file
            /// </summary> 
            public static void GetXMPProperties()
            {
                try
                {
                    //ExStart:GetXMPPropertiesPSDFormat
                    // initialize PsdFormat 
                    PsdFormat psdFormat = new PsdFormat(Common.MapSourceFilePath(filePath));

                    // get photoshop namespace
                    var photoshopMetadata = psdFormat.XmpValues.Schemes.Photoshop;

                    // get color mode
                    ColorMode colorMode = (ColorMode)photoshopMetadata.ColorMode;

                    // get IIC profile
                    var iicProfile = photoshopMetadata.ICCProfile;
                    //ExEnd:GetXMPPropertiesPSDFormat
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }

        public static class Cad
        {
            // initialize file path
            //ExStart:SourceCADFilePath
            private const string dxfFilePath = "Images/Cad/sample.dxf";
            private const string dwgFilePath = "Images/Cad/sample.dwg";
            //ExEnd:SourceCADFilePath

            /// <summary>
            /// Reads metadata from dwg file
            /// </summary> 
            public static void GetMetadatPropertiesInDWG()
            {
                try
                {
                    //ExStart:GetMetadatPropertiesInDWG 
                    // initialize DwgFormat class
                    DwgFormat dwgFormat = new DwgFormat(Common.MapSourceFilePath(dwgFilePath));

                    // get metadata
                    CadMetadata metadata = dwgFormat.GetDwgMetadata();

                    // get width
                    int width = metadata.Width;

                    // get height
                    int height = metadata.Height;

                    // get header attribute
                    string[] attributes = metadata.HeaderAttributes;
                    //ExEnd:GetMetadatPropertiesInDWG
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
            /// <summary>
            /// Reads metadata from dxf file
            /// </summary> 
            public static void GetMetadatPropertiesInDXF()
            {
                try
                {
                    //ExStart:GetMetadatPropertiesInDXF 
                    // initialize DwgFormat class
                    DxfFormat dxfFormat = new DxfFormat(Common.MapSourceFilePath(dxfFilePath));

                    // get metadata
                    CadMetadata metadata = dxfFormat.GetDfxMetadata();

                    // get width
                    int width = metadata.Width;

                    // get height
                    int height = metadata.Height;

                    // get header attribute
                    string[] attributes = metadata.HeaderAttributes;
                    //ExEnd:GetMetadatPropertiesInDXF
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
    }
}
