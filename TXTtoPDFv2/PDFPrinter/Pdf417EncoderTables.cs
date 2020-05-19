﻿/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Static Tables class
//
//	Author: Uzi Granot
//	Version: 2.0
//	Date: May 7, 2019
//	Copyright (C) 2019 Uzi Granot. All Rights Reserved
//
//	PDF417 barcode encoder class and the attached test/demo
//  applications are free software.
//	Software developed by this author is licensed under CPOL 1.02.
//
//	The main points of CPOL 1.02 subject to the terms of the License are:
//
//	Source Code and Executable Files can be used in commercial applications;
//	Source Code and Executable Files can be redistributed; and
//	Source Code can be modified to create derivative works.
//	No claim of suitability, guarantee, or any warranty whatsoever is
//	provided. The software is provided "as-is".
//	The Article accompanying the Work may not be distributed or republished
//	without the Author's consent
//
//	Version History
//	---------------
//
//	Version 1.0 2019/04/01
//		Original version
/////////////////////////////////////////////////////////////////////

using System.Numerics;

//namespace Pdf417EncoderLibrary
namespace PdfFileWriter
{
/// <summary>
/// Static tables for Pdf417Encoder
/// </summary>
public class Pdf417EncoderTables
	{
	// powers of 900
	internal static readonly long[] Fact900 = {1, 900, 810000, 729000000, 656100000000};

	// powers of big iteger 900
	internal static readonly BigInteger[] FactBigInt900 = new BigInteger[15];

	static Pdf417EncoderTables()
		{
		// factors for big integer 900
		FactBigInt900[0] = BigInteger.One;
		for(int Index = 1; Index < 15; Index++)
			{
			FactBigInt900[Index] = 900 * FactBigInt900[Index - 1];
			}
		return;
		}

	internal static readonly byte[] TextToUpper =
		{
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		26, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 
		15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		};

	internal static readonly byte[] TextToLower =
		{
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		26, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 
		15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 127, 127, 127, 127, 127, 
		};

	internal static readonly byte[] TextToMixed =
		{
		127, 127, 127, 127, 127, 127, 127, 127, 127, 12, 127, 127, 127, 11, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		26, 127, 127, 15, 18, 21, 10, 127, 127, 127, 22, 20, 13, 16, 17, 19, 
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 14, 127, 127, 23, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 24, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		};

	internal static readonly byte[] TextToPunct =
		{
		127, 127, 127, 127, 127, 127, 127, 127, 127, 12, 15, 127, 127, 11, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 10, 20, 127, 18, 127, 127, 28, 23, 24, 22, 127, 13, 16, 17, 19, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 14, 0, 1, 127, 2, 25, 
		3, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 4, 5, 6, 127, 7, 
		8, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 
		127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 26, 21, 27, 9, 127, 
		};

	// Tables of coefficients for calculating error correction words
	// (see annex F, ISO/IEC 15438:2001(E))
	internal static readonly int[][] ErrorCorrectionTables =
		{
		new int [] {27, 917},
		new int [] {522, 568, 723, 809},
		new int [] {237, 308, 436, 284, 646, 653, 428, 379},
		new int []
			{
			274, 562, 232, 755, 599, 524, 801, 132, 295, 116, 442, 428, 295,
			42, 176, 65
			},
		new int []
			{
			361, 575, 922, 525, 176, 586, 640, 321, 536, 742, 677, 742, 687,
			284, 193, 517, 273, 494, 263, 147, 593, 800, 571, 320, 803,
			133, 231, 390, 685, 330, 63, 410
			},
		new int []
			{
			539, 422, 6, 93, 862, 771, 453, 106, 610, 287, 107, 505, 733,
			877, 381, 612, 723, 476, 462, 172, 430, 609, 858, 822, 543,
			376, 511, 400, 672, 762, 283, 184, 440, 35, 519, 31, 460,
			594, 225, 535, 517, 352, 605, 158, 651, 201, 488, 502, 648,
			733, 717, 83, 404, 97, 280, 771, 840, 629, 4, 381, 843,
			623, 264, 543
			},
		new int []
			{
			521, 310, 864, 547, 858, 580, 296, 379, 53, 779, 897, 444, 400,
			925, 749, 415, 822, 93, 217, 208, 928, 244, 583, 620, 246,
			148, 447, 631, 292, 908, 490, 704, 516, 258, 457, 907, 594,
			723, 674, 292, 272, 96, 684, 432, 686, 606, 860, 569, 193,
			219, 129, 186, 236, 287, 192, 775, 278, 173, 40, 379, 712,
			463, 646, 776, 171, 491, 297, 763, 156, 732, 95, 270, 447,
			90, 507, 48, 228, 821, 808, 898, 784, 663, 627, 378, 382,
			262, 380, 602, 754, 336, 89, 614, 87, 432, 670, 616, 157,
			374, 242, 726, 600, 269, 375, 898, 845, 454, 354, 130, 814,
			587, 804, 34, 211, 330, 539, 297, 827, 865, 37, 517, 834,
			315, 550, 86, 801, 4, 108, 539
			},
		new int []
			{
			524, 894, 75, 766, 882, 857, 74, 204, 82, 586, 708, 250, 905,
			786, 138, 720, 858, 194, 311, 913, 275, 190, 375, 850, 438,
			733, 194, 280, 201, 280, 828, 757, 710, 814, 919, 89, 68,
			569, 11, 204, 796, 605, 540, 913, 801, 700, 799, 137, 439,
			418, 592, 668, 353, 859, 370, 694, 325, 240, 216, 257, 284,
			549, 209, 884, 315, 70, 329, 793, 490, 274, 877, 162, 749,
			812, 684, 461, 334, 376, 849, 521, 307, 291, 803, 712, 19,
			358, 399, 908, 103, 511, 51, 8, 517, 225, 289, 470, 637,
			731, 66, 255, 917, 269, 463, 830, 730, 433, 848, 585, 136,
			538, 906, 90, 2, 290, 743, 199, 655, 903, 329, 49, 802,
			580, 355, 588, 188, 462, 10, 134, 628, 320, 479, 130, 739,
			71, 263, 318, 374, 601, 192, 605, 142, 673, 687, 234, 722,
			384, 177, 752, 607, 640, 455, 193, 689, 707, 805, 641, 48,
			60, 732, 621, 895, 544, 261, 852, 655, 309, 697, 755, 756,
			60, 231, 773, 434, 421, 726, 528, 503, 118, 49, 795, 32,
			144, 500, 238, 836, 394, 280, 566, 319, 9, 647, 550, 73,
			914, 342, 126, 32, 681, 331, 792, 620, 60, 609, 441, 180,
			791, 893, 754, 605, 383, 228, 749, 760, 213, 54, 297, 134,
			54, 834, 299, 922, 191, 910, 532, 609, 829, 189, 20, 167,
			29, 872, 449, 83, 402, 41, 656, 505, 579, 481, 173, 404,
			251, 688, 95, 497, 555, 642, 543, 307, 159, 924, 558, 648,
			55, 497, 10
			},
		new int []
			{
			352, 77, 373, 504, 35, 599, 428, 207, 409, 574, 118, 498, 285,
			380, 350, 492, 197, 265, 920, 155, 914, 299, 229, 643, 294,
			871, 306, 88, 87, 193, 352, 781, 846, 75, 327, 520, 435,
			543, 203, 666, 249, 346, 781, 621, 640, 268, 794, 534, 539,
			781, 408, 390, 644, 102, 476, 499, 290, 632, 545, 37, 858,
			916, 552, 41, 542, 289, 122, 272, 383, 800, 485, 98, 752,
			472, 761, 107, 784, 860, 658, 741, 290, 204, 681, 407, 855,
			85, 99, 62, 482, 180, 20, 297, 451, 593, 913, 142, 808,
			684, 287, 536, 561, 76, 653, 899, 729, 567, 744, 390, 513,
			192, 516, 258, 240, 518, 794, 395, 768, 848, 51, 610, 384,
			168, 190, 826, 328, 596, 786, 303, 570, 381, 415, 641, 156,
			237, 151, 429, 531, 207, 676, 710, 89, 168, 304, 402, 40,
			708, 575, 162, 864, 229, 65, 861, 841, 512, 164, 477, 221,
			92, 358, 785, 288, 357, 850, 836, 827, 736, 707, 94, 8,
			494, 114, 521, 2, 499, 851, 543, 152, 729, 771, 95, 248,
			361, 578, 323, 856, 797, 289, 51, 684, 466, 533, 820, 669,
			45, 902, 452, 167, 342, 244, 173, 35, 463, 651, 51, 699,
			591, 452, 578, 37, 124, 298, 332, 552, 43, 427, 119, 662,
			777, 475, 850, 764, 364, 578, 911, 283, 711, 472, 420, 245,
			288, 594, 394, 511, 327, 589, 777, 699, 688, 43, 408, 842,
			383, 721, 521, 560, 644, 714, 559, 62, 145, 873, 663, 713,
			159, 672, 729, 624, 59, 193, 417, 158, 209, 563, 564, 343,
			693, 109, 608, 563, 365, 181, 772, 677, 310, 248, 353, 708,
			410, 579, 870, 617, 841, 632, 860, 289, 536, 35, 777, 618,
			586, 424, 833, 77, 597, 346, 269, 757, 632, 695, 751, 331,
			247, 184, 45, 787, 680, 18, 66, 407, 369, 54, 492, 228,
			613, 830, 922, 437, 519, 644, 905, 789, 420, 305, 441, 207,
			300, 892, 827, 141, 537, 381, 662, 513, 56, 252, 341, 242,
			797, 838, 837, 720, 224, 307, 631, 61, 87, 560, 310, 756,
			665, 397, 808, 851, 309, 473, 795, 378, 31, 647, 915, 459,
			806, 590, 731, 425, 216, 548, 249, 321, 881, 699, 535, 673,
			782, 210, 815, 905, 303, 843, 922, 281, 73, 469, 791, 660,
			162, 498, 308, 155, 422, 907, 817, 187, 62, 16, 425, 535,
			336, 286, 437, 375, 273, 610, 296, 183, 923, 116, 667, 751,
			353, 62, 366, 691, 379, 687, 842, 37, 357, 720, 742, 330,
			5, 39, 923, 311, 424, 242, 749, 321, 54, 669, 316, 342,
			299, 534, 105, 667, 488, 640, 672, 576, 540, 316, 486, 721,
			610, 46, 656, 447, 171, 616, 464, 190, 531, 297, 321, 762,
			752, 533, 175, 134, 14, 381, 433, 717, 45, 111, 20, 596,
			284, 736, 138, 646, 411, 877, 669, 141, 919, 45, 780, 407,
			164, 332, 899, 165, 726, 600, 325, 498, 655, 357, 752, 768,
			223, 849, 647, 63, 310, 863, 251, 366, 304, 282, 738, 675,
			410, 389, 244, 31, 121, 303, 263
			}
		};

	// Codewords table
	// There are 929 codewords (0 to 928).
	// Each codeword is represented by 17 black and white bars.
	// The first bar (on the left) is always black and
	// the last bar (on the right) is always white.
	// each table entry has 15 bars, the first and last are missing.
	// The translation table is made of three sub-tables one for each cluster.
	// Each sub-table has 929 entries.
	internal static readonly short[,] CodewordTable =
		{
		{
		0x6ae0, 0x7578, 0x7abe, 0x6a70, 0x753c, 0x7a9f, 0x5460, 0x6a38, 0x5430, 0x2820, 
		0x5418, 0x2810, 0x56e0, 0x6b78, 0x75be, 0x5670, 0x6b3c, 0x759f, 0x2c60, 0x5638, 
		0x2c30, 0x2ee0, 0x5778, 0x6bbe, 0x2e70, 0x573c, 0x6b9f, 0x2e38, 0x571e, 0x2f78, 
		0x57be, 0x2f3c, 0x579f, 0x2fbe, 0x7afd, 0x6970, 0x74bc, 0x7a5f, 0x5260, 0x6938, 
		0x749e, 0x5230, 0x691c, 0x2420, 0x5218, 0x690e, 0x2410, 0x520c, 0x2408, 0x5370, 
		0x69bc, 0x74df, 0x2660, 0x5338, 0x699e, 0x2630, 0x531c, 0x698f, 0x2618, 0x530e, 
		0x2770, 0x53bc, 0x69df, 0x2738, 0x539e, 0x271c, 0x538f, 0x27bc, 0x53df, 0x279e, 
		0x278f, 0x5160, 0x68b8, 0x745e, 0x5130, 0x689c, 0x744f, 0x2220, 0x5118, 0x688e, 
		0x2210, 0x510c, 0x2208, 0x2204, 0x2360, 0x51b8, 0x68de, 0x2330, 0x519c, 0x68cf, 
		0x2318, 0x518e, 0x230c, 0x2306, 0x23b8, 0x51de, 0x239c, 0x51cf, 0x238e, 0x23de, 
		0x50b0, 0x685c, 0x742f, 0x2120, 0x5098, 0x684e, 0x2110, 0x508c, 0x6847, 0x2108, 
		0x5086, 0x2104, 0x5083, 0x21b0, 0x50dc, 0x686f, 0x2198, 0x50ce, 0x218c, 0x50c7, 
		0x2186, 0x2183, 0x50ef, 0x21c7, 0x20a0, 0x5058, 0x682e, 0x2090, 0x504c, 0x6827, 
		0x2088, 0x5046, 0x2084, 0x5043, 0x2082, 0x20d8, 0x20cc, 0x20c6, 0x2050, 0x6817, 
		0x5026, 0x5023, 0x2041, 0x6570, 0x72bc, 0x795f, 0x4a60, 0x6538, 0x729e, 0x4a30, 
		0x651c, 0x728f, 0x1420, 0x4a18, 0x1410, 0x4b70, 0x65bc, 0x72df, 0x1660, 0x4b38, 
		0x659e, 0x1630, 0x4b1c, 0x1618, 0x160c, 0x1770, 0x4bbc, 0x65df, 0x1738, 0x4b9e, 
		0x171c, 0x170e, 0x17bc, 0x4bdf, 0x179e, 0x17df, 0x6d60, 0x76b8, 0x7b5e, 0x6d30, 
		0x769c, 0x7b4f, 0x5a20, 0x6d18, 0x768e, 0x5a10, 0x6d0c, 0x7687, 0x5a08, 0x6d06, 
		0x4960, 0x64b8, 0x725e, 0x5b60, 0x4930, 0x649c, 0x724f, 0x5b30, 0x6d9c, 0x76cf, 
		0x3620, 0x1210, 0x490c, 0x6487, 0x3610, 0x5b0c, 0x3608, 0x1360, 0x49b8, 0x64de, 
		0x3760, 0x1330, 0x499c, 0x64cf, 0x3730, 0x5b9c, 0x6dcf, 0x3718, 0x130c, 0x370c, 
		0x13b8, 0x49de, 0x37b8, 0x139c, 0x49cf, 0x379c, 0x5bcf, 0x378e, 0x13de, 0x37de, 
		0x13cf, 0x37cf, 0x6cb0, 0x765c, 0x7b2f, 0x5920, 0x6c98, 0x764e, 0x5910, 0x6c8c, 
		0x7647, 0x5908, 0x6c86, 0x5904, 0x5902, 0x48b0, 0x645c, 0x722f, 0x59b0, 0x4898, 
		0x644e, 0x3320, 0x1110, 0x6cce, 0x6447, 0x3310, 0x1108, 0x4886, 0x3308, 0x5986, 
		0x4883, 0x1102, 0x11b0, 0x48dc, 0x646f, 0x33b0, 0x1198, 0x48ce, 0x3398, 0x59ce, 
		0x48c7, 0x338c, 0x1186, 0x1183, 0x11dc, 0x48ef, 0x33dc, 0x11ce, 0x33ce, 0x11c7, 
		0x33c7, 0x33ef, 0x58a0, 0x6c58, 0x762e, 0x5890, 0x6c4c, 0x7627, 0x5888, 0x6c46, 
		0x5884, 0x6c43, 0x5882, 0x5881, 0x10a0, 0x4858, 0x642e, 0x31a0, 0x1090, 0x484c, 
		0x6427, 0x3190, 0x58cc, 0x6c67, 0x3188, 0x1084, 0x4843, 0x3184, 0x58c3, 0x3182, 
		0x10d8, 0x486e, 0x31d8, 0x10cc, 0x4867, 0x31cc, 0x58e7, 0x31c6, 0x10c3, 0x31c3, 
		0x31ee, 0x31e7, 0x5850, 0x6c2c, 0x7617, 0x5848, 0x6c26, 0x5844, 0x6c23, 0x5842, 
		0x5841, 0x1050, 0x482c, 0x6417, 0x30d0, 0x1048, 0x4826, 0x30c8, 0x5866, 0x4823, 
		0x30c4, 0x1042, 0x30c2, 0x1041, 0x106c, 0x30ec, 0x30e6, 0x30e3, 0x6c16, 0x6c13, 
		0x5821, 0x4816, 0x1024, 0x3064, 0x3062, 0x3061, 0x4560, 0x62b8, 0x715e, 0x4530, 
		0x629c, 0x0a20, 0x4518, 0x628e, 0x0a10, 0x450c, 0x0a08, 0x0a04, 0x0b60, 0x45b8, 
		0x62de, 0x0b30, 0x459c, 0x62cf, 0x0b18, 0x458e, 0x0b0c, 0x0b06, 0x0bb8, 0x45de, 
		0x0b9c, 0x45cf, 0x0b8e, 0x0bde, 0x0bcf, 0x66b0, 0x735c, 0x79af, 0x4d20, 0x6698, 
		0x734e, 0x4d10, 0x668c, 0x7347, 0x4d08, 0x6686, 0x4d04, 0x6683, 0x44b0, 0x625c, 
		0x712f, 0x4db0, 0x4498, 0x624e, 0x1b20, 0x0910, 0x66ce, 0x6247, 0x1b10, 0x4d8c, 
		0x4486, 0x1b08, 0x0904, 0x1b04, 0x09b0, 0x44dc, 0x626f, 0x1bb0, 0x0998, 0x66ef, 
		0x1b98, 0x4dce, 0x44c7, 0x1b8c, 0x0986, 0x1b86, 0x09dc, 0x44ef, 0x1bdc, 0x09ce, 
		0x1bce, 0x09c7, 0x09ef, 0x1bef, 0x6ea0, 0x7758, 0x7bae, 0x6e90, 0x774c, 0x7ba7, 
		0x6e88, 0x7746, 0x6e84, 0x7743, 0x6e82, 0x4ca0, 0x6658, 0x732e, 0x5da0, 0x4c90, 
		0x776e, 0x7327, 0x5d90, 0x6ecc, 0x7767, 0x5d88, 0x4c84, 0x6643, 0x5d84, 0x6ec3, 
		0x4c81, 0x08a0, 0x4458, 0x622e, 0x19a0, 0x0890, 0x444c, 0x6227, 0x3ba0, 0x1990, 
		0x4ccc, 0x6667, 0x3b90, 0x5dcc, 0x6ee7, 0x4443, 0x3b88, 0x1984, 0x4cc3, 0x3b84, 
		0x0881, 0x08d8, 0x446e, 0x19d8, 0x08cc, 0x4467, 0x3bd8, 0x19cc, 0x4ce7, 0x3bcc, 
		0x5de7, 0x08c3, 0x19c3, 0x08ee, 0x19ee, 0x08e7, 0x3bee, 0x19e7, 0x6e50, 0x772c, 
		0x7b97, 0x6e48, 0x7726, 0x6e44, 0x7723, 0x6e42, 0x6e41, 0x4c50, 0x662c, 0x7317, 
		0x5cd0, 0x4c48, 0x7737, 0x5cc8, 0x6e66, 0x6623, 0x5cc4, 0x4c42, 0x5cc2, 0x4c41, 
		0x5cc1, 0x0850, 0x442c, 0x6217, 0x18d0, 0x0848, 0x4426, 0x39d0, 0x18c8, 0x4c66, 
		0x4423, 0x39c8, 0x5ce6, 0x0842, 0x39c4, 0x18c2, 0x0841, 0x18c1, 0x086c, 0x4437, 
		0x18ec, 0x0866, 0x39ec, 0x18e6, 0x0863, 0x39e6, 0x18e3, 0x0877, 0x39f7, 0x6e28, 
		0x7716, 0x6e24, 0x7713, 0x6e22, 0x6e21, 0x4c28, 0x6616, 0x5c68, 0x4c24, 0x6613, 
		0x5c64, 0x6e33, 0x5c62, 0x4c21, 0x5c61, 0x0828, 0x4416, 0x1868, 0x0824, 0x4413, 
		0x38e8, 0x1864, 0x4c33, 0x38e4, 0x5c73, 0x0821, 0x38e2, 0x1861, 0x38e1, 0x1876, 
		0x38f6, 0x38f3, 0x770b, 0x6e11, 0x660b, 0x4c12, 0x4c11, 0x0814, 0x1834, 0x3874, 
		0x0811, 0x1831, 0x42b0, 0x0520, 0x4298, 0x0510, 0x428c, 0x6147, 0x0508, 0x4286, 
		0x0504, 0x4283, 0x05b0, 0x42dc, 0x616f, 0x0598, 0x42ce, 0x058c, 0x42c7, 0x0586, 
		0x0583, 0x05dc, 0x42ef, 0x05ce, 0x05c7, 0x05ef, 0x46a0, 0x6358, 0x71ae, 0x4690, 
		0x634c, 0x4688, 0x6346, 0x4684, 0x6343, 0x4682, 0x04a0, 0x4258, 0x612e, 0x0da0, 
		0x0490, 0x636e, 0x6127, 0x0d90, 0x46cc, 0x6367, 0x0d88, 0x0484, 0x4243, 0x0d84, 
		0x46c3, 0x0481, 0x04d8, 0x426e, 0x0dd8, 0x04cc, 0x4267, 0x0dcc, 0x46e7, 0x0dc6, 
		0x04c3, 0x04ee, 0x0dee, 0x04e7, 0x0de7, 0x6750, 0x73ac, 0x79d7, 0x6748, 0x73a6, 
		0x6744, 0x73a3, 0x6742, 0x6741, 0x4650, 0x632c, 0x4ed0, 0x4648, 0x6326, 0x4ec8, 
		0x6766, 0x6323, 0x4ec4, 0x4642, 0x4ec2, 0x4641, 0x4ec1, 0x0450, 0x422c, 0x0cd0, 
		0x0448, 0x6337, 0x1dd0, 0x0cc8, 0x4666, 0x4223, 0x1dc8, 0x4ee6, 0x0442, 0x1dc4, 
		0x0cc2, 0x0441, 0x0cc1, 0x046c, 0x4237, 0x0cec, 0x0466, 0x1dec, 0x0ce6, 0x0463, 
		0x1de6, 0x0ce3, 0x0477, 0x0cf7, 0x1df7, 0x77a8, 0x7bd6, 0x77a4, 0x7bd3, 0x77a2, 
		0x77a1, 0x6728, 0x7396, 0x6f68, 0x77b6, 0x7393, 0x6f64, 0x77b3, 0x6f62, 0x6721, 
		0x6f61, 0x4628, 0x6316, 0x4e68, 0x4624, 0x6313, 0x5ee8, 0x4e64, 0x6733, 0x5ee4, 
		0x6f73, 0x4621, 0x5ee2, 0x4e61, 0x5ee1, 0x0428, 0x4216, 0x0c68, 0x0424, 0x4213, 
		0x1ce8, 0x0c64, 0x4633, 0x3de8, 0x1ce4, 0x4e73, 0x0421, 0x3de4, 0x5ef3, 0x0c61, 
		0x3de2, 0x0436, 0x0c76, 0x0433, 0x1cf6, 0x0c73, 0x3df6, 0x1cf3, 0x3df3, 0x7794, 
		0x7bcb, 0x7792, 0x7791, 0x6714, 0x738b, 0x6f34, 0x779b, 0x6f32, 0x6711, 0x6f31, 
		0x4614, 0x630b, 0x4e34, 0x4612, 0x5e74, 0x4e32, 0x4611, 0x5e72, 0x4e31, 0x5e71, 
		0x0414, 0x420b, 0x0c34, 0x461b, 0x1c74, 0x0c32, 0x0411, 0x3cf4, 0x1c72, 0x0c31, 
		0x3cf2, 0x1c71, 0x3cf1, 0x0c3b, 0x3cfb, 0x7789, 0x6f1a, 0x6f19, 0x4e1a, 0x5e3a, 
		0x5e39, 0x0c1a, 0x1c3a, 0x3c7a, 0x3c79, 0x02a0, 0x0290, 0x414c, 0x0288, 0x0284, 
		0x0282, 0x02d8, 0x02cc, 0x02c6, 0x02c3, 0x02ee, 0x02e7, 0x4350, 0x4348, 0x61a6, 
		0x4344, 0x61a3, 0x4342, 0x4341, 0x0250, 0x412c, 0x06d0, 0x436c, 0x4126, 0x06c8, 
		0x4366, 0x06c4, 0x4363, 0x06c2, 0x0241, 0x06c1, 0x026c, 0x4137, 0x06ec, 0x4377, 
		0x06e6, 0x0263, 0x06e3, 0x0277, 0x06f7, 0x63a8, 0x63a4, 0x63a2, 0x63a1, 0x4328, 
		0x4768, 0x63b6, 0x6193, 0x4764, 0x63b3, 0x4762, 0x4321, 0x4761, 0x0228, 0x0668, 
		0x0224, 0x4113, 0x0ee8, 0x0664, 0x0222, 0x0ee4, 0x0662, 0x0221, 0x0ee2, 0x0661, 
		0x0236, 0x0676, 0x0233, 0x0ef6, 0x0673, 0x0ef3, 0x73d4, 0x73d2, 0x73d1, 0x6394, 
		0x67b4, 0x73db, 0x67b2, 0x6391, 0x67b1, 0x4314, 0x618b, 0x4734, 0x639b, 0x4f74, 
		0x4732, 0x4311, 0x4f72, 0x4731, 0x4f71, 0x0214, 0x410b, 0x0634, 0x431b, 0x0e74, 
		0x0632, 0x0211, 0x1ef4, 0x0e72, 0x0631, 0x1ef2, 0x0e71, 0x021b, 0x063b, 0x0e7b, 
		0x1efb, 0x7bea, 0x7be9, 0x73ca, 0x77da, 0x73c9, 0x77d9, 0x638a, 0x679a, 0x6389, 
		0x6fba, 0x6799, 0x6fb9, 0x430a, 0x471a, 0x4309, 0x4f3a, 0x4719, 0x5f7a, 
		},
		{
		0x7ab0, 0x7d5c, 0x7520, 0x7a98, 0x7d4e, 0x7510, 0x7a8c, 0x7d47, 0x7508, 0x7a86, 
		0x7504, 0x7a83, 0x7502, 0x75b0, 0x7adc, 0x7d6f, 0x6b20, 0x7598, 0x7ace, 0x6b10, 
		0x758c, 0x7ac7, 0x6b08, 0x7586, 0x6b04, 0x7583, 0x6b02, 0x6bb0, 0x75dc, 0x7aef, 
		0x5720, 0x6b98, 0x75ce, 0x5710, 0x6b8c, 0x75c7, 0x5708, 0x6b86, 0x5704, 0x6b83, 
		0x5702, 0x57b0, 0x6bdc, 0x75ef, 0x2f20, 0x5798, 0x6bce, 0x2f10, 0x578c, 0x6bc7, 
		0x2f08, 0x5786, 0x2f04, 0x5783, 0x2fb0, 0x57dc, 0x6bef, 0x2f98, 0x57ce, 0x2f8c, 
		0x57c7, 0x2f86, 0x2fdc, 0x57ef, 0x2fce, 0x2fc7, 0x74a0, 0x7a58, 0x7d2e, 0x7490, 
		0x7a4c, 0x7d27, 0x7488, 0x7a46, 0x7484, 0x7a43, 0x7482, 0x7481, 0x69a0, 0x74d8, 
		0x7a6e, 0x6990, 0x74cc, 0x7a67, 0x6988, 0x74c6, 0x6984, 0x74c3, 0x6982, 0x6981, 
		0x53a0, 0x69d8, 0x74ee, 0x5390, 0x69cc, 0x74e7, 0x5388, 0x69c6, 0x5384, 0x69c3, 
		0x5382, 0x5381, 0x27a0, 0x53d8, 0x69ee, 0x2790, 0x53cc, 0x69e7, 0x2788, 0x53c6, 
		0x2784, 0x53c3, 0x2782, 0x27d8, 0x53ee, 0x27cc, 0x53e7, 0x27c6, 0x27c3, 0x27ee, 
		0x27e7, 0x7450, 0x7a2c, 0x7d17, 0x7448, 0x7a26, 0x7444, 0x7a23, 0x7442, 0x7441, 
		0x68d0, 0x746c, 0x7a37, 0x68c8, 0x7466, 0x68c4, 0x7463, 0x68c2, 0x68c1, 0x51d0, 
		0x68ec, 0x7477, 0x51c8, 0x68e6, 0x51c4, 0x68e3, 0x51c2, 0x51c1, 0x23d0, 0x51ec, 
		0x68f7, 0x23c8, 0x51e6, 0x23c4, 0x51e3, 0x23c2, 0x23c1, 0x23ec, 0x51f7, 0x23e6, 
		0x23e3, 0x23f7, 0x7428, 0x7a16, 0x7424, 0x7a13, 0x7422, 0x7421, 0x6868, 0x7436, 
		0x6864, 0x7433, 0x6862, 0x6861, 0x50e8, 0x6876, 0x50e4, 0x6873, 0x50e2, 0x50e1, 
		0x21e8, 0x50f6, 0x21e4, 0x50f3, 0x21e2, 0x21e1, 0x21f6, 0x21f3, 0x7414, 0x7a0b, 
		0x7412, 0x7411, 0x6834, 0x741b, 0x6832, 0x6831, 0x5074, 0x683b, 0x5072, 0x5071, 
		0x20f4, 0x507b, 0x20f2, 0x20f1, 0x740a, 0x7409, 0x681a, 0x6819, 0x503a, 0x5039, 
		0x72a0, 0x7958, 0x7cae, 0x7290, 0x794c, 0x7ca7, 0x7288, 0x7946, 0x7284, 0x7943, 
		0x7282, 0x7281, 0x65a0, 0x72d8, 0x796e, 0x6590, 0x72cc, 0x7967, 0x6588, 0x72c6, 
		0x6584, 0x72c3, 0x6582, 0x6581, 0x4ba0, 0x65d8, 0x72ee, 0x4b90, 0x65cc, 0x72e7, 
		0x4b88, 0x65c6, 0x4b84, 0x65c3, 0x4b82, 0x4b81, 0x17a0, 0x4bd8, 0x65ee, 0x1790, 
		0x4bcc, 0x65e7, 0x1788, 0x4bc6, 0x1784, 0x4bc3, 0x1782, 0x17d8, 0x4bee, 0x17cc, 
		0x4be7, 0x17c6, 0x17c3, 0x17ee, 0x17e7, 0x7b50, 0x7dac, 0x35f8, 0x7b48, 0x7da6, 
		0x34fc, 0x7b44, 0x7da3, 0x347e, 0x7b42, 0x7b41, 0x7250, 0x792c, 0x7c97, 0x76d0, 
		0x7248, 0x7db7, 0x76c8, 0x7b66, 0x7923, 0x76c4, 0x7242, 0x76c2, 0x7241, 0x76c1, 
		0x64d0, 0x726c, 0x7937, 0x6dd0, 0x64c8, 0x7266, 0x6dc8, 0x76e6, 0x7263, 0x6dc4, 
		0x64c2, 0x6dc2, 0x64c1, 0x6dc1, 0x49d0, 0x64ec, 0x7277, 0x5bd0, 0x49c8, 0x64e6, 
		0x5bc8, 0x6de6, 0x64e3, 0x5bc4, 0x49c2, 0x5bc2, 0x49c1, 0x5bc1, 0x13d0, 0x49ec, 
		0x64f7, 0x37d0, 0x13c8, 0x49e6, 0x37c8, 0x5be6, 0x49e3, 0x37c4, 0x13c2, 0x37c2, 
		0x13c1, 0x13ec, 0x49f7, 0x37ec, 0x13e6, 0x37e6, 0x13e3, 0x37e3, 0x13f7, 0x7b28, 
		0x7d96, 0x32fc, 0x7b24, 0x7d93, 0x327e, 0x7b22, 0x323f, 0x7b21, 0x7228, 0x7916, 
		0x7668, 0x7224, 0x7913, 0x7664, 0x7b33, 0x7662, 0x7221, 0x7661, 0x6468, 0x7236, 
		0x6ce8, 0x6464, 0x7233, 0x6ce4, 0x7673, 0x6ce2, 0x6461, 0x6ce1, 0x48e8, 0x6476, 
		0x59e8, 0x48e4, 0x6473, 0x59e4, 0x6cf3, 0x59e2, 0x48e1, 0x59e1, 0x11e8, 0x48f6, 
		0x33e8, 0x11e4, 0x48f3, 0x33e4, 0x59f3, 0x33e2, 0x11e1, 0x33e1, 0x11f6, 0x33f6, 
		0x11f3, 0x33f3, 0x7b14, 0x7d8b, 0x317e, 0x7b12, 0x313f, 0x7b11, 0x7214, 0x790b, 
		0x7634, 0x7b1b, 0x7632, 0x7211, 0x7631, 0x6434, 0x721b, 0x6c74, 0x6432, 0x6c72, 
		0x6431, 0x6c71, 0x4874, 0x643b, 0x58f4, 0x6c7b, 0x58f2, 0x4871, 0x58f1, 0x10f4, 
		0x487b, 0x31f4, 0x10f2, 0x31f2, 0x10f1, 0x31f1, 0x10fb, 0x31fb, 0x7b0a, 0x30bf, 
		0x7b09, 0x720a, 0x761a, 0x7209, 0x7619, 0x641a, 0x6c3a, 0x6419, 0x6c39, 0x483a, 
		0x587a, 0x4839, 0x5879, 0x107a, 0x30fa, 0x1079, 0x30f9, 0x7b05, 0x7205, 0x760d, 
		0x640d, 0x6c1d, 0x481d, 0x583d, 0x7150, 0x78ac, 0x7c57, 0x7148, 0x78a6, 0x7144, 
		0x78a3, 0x7142, 0x7141, 0x62d0, 0x716c, 0x78b7, 0x62c8, 0x7166, 0x62c4, 0x7163, 
		0x62c2, 0x62c1, 0x45d0, 0x62ec, 0x7177, 0x45c8, 0x62e6, 0x45c4, 0x62e3, 0x45c2, 
		0x45c1, 0x0bd0, 0x45ec, 0x62f7, 0x0bc8, 0x45e6, 0x0bc4, 0x45e3, 0x0bc2, 0x0bc1, 
		0x0bec, 0x45f7, 0x0be6, 0x0be3, 0x0bf7, 0x79a8, 0x7cd6, 0x1afc, 0x79a4, 0x7cd3, 
		0x1a7e, 0x79a2, 0x1a3f, 0x79a1, 0x7128, 0x7896, 0x7368, 0x7124, 0x7893, 0x7364, 
		0x79b3, 0x7362, 0x7121, 0x7361, 0x6268, 0x7136, 0x66e8, 0x6264, 0x7133, 0x66e4, 
		0x7373, 0x66e2, 0x6261, 0x66e1, 0x44e8, 0x6276, 0x4de8, 0x44e4, 0x6273, 0x4de4, 
		0x66f3, 0x4de2, 0x44e1, 0x4de1, 0x09e8, 0x44f6, 0x1be8, 0x09e4, 0x44f3, 0x1be4, 
		0x4df3, 0x1be2, 0x09e1, 0x1be1, 0x09f6, 0x1bf6, 0x09f3, 0x1bf3, 0x7dd4, 0x3af8, 
		0x5d7e, 0x7dd2, 0x3a7c, 0x5d3f, 0x7dd1, 0x3a3e, 0x3a1f, 0x7994, 0x7ccb, 0x197e, 
		0x7bb4, 0x7ddb, 0x3b7e, 0x193f, 0x7bb2, 0x7991, 0x3b3f, 0x7bb1, 0x7114, 0x788b, 
		0x7334, 0x7112, 0x7774, 0x7bbb, 0x7111, 0x7772, 0x7331, 0x7771, 0x6234, 0x711b, 
		0x6674, 0x6232, 0x6ef4, 0x6672, 0x6231, 0x6ef2, 0x6671, 0x6ef1, 0x4474, 0x623b, 
		0x4cf4, 0x4472, 0x5df4, 0x4cf2, 0x4471, 0x5df2, 0x4cf1, 0x5df1, 0x08f4, 0x447b, 
		0x19f4, 0x08f2, 0x3bf4, 0x19f2, 0x08f1, 0x3bf2, 0x19f1, 0x3bf1, 0x08fb, 0x19fb, 
		0x7dca, 0x397c, 0x5cbf, 0x7dc9, 0x393e, 0x391f, 0x798a, 0x18bf, 0x7b9a, 0x7989, 
		0x39bf, 0x7b99, 0x710a, 0x731a, 0x7109, 0x773a, 0x7319, 0x7739, 0x621a, 0x663a, 
		0x6219, 0x6e7a, 0x6639, 0x6e79, 0x443a, 0x4c7a, 0x4439, 0x5cfa, 0x4c79, 0x5cf9, 
		0x087a, 0x18fa, 0x0879, 0x39fa, 0x18f9, 0x39f9, 0x7dc5, 0x38be, 0x389f, 0x7985, 
		0x7b8d, 0x7105, 0x730d, 0x771d, 0x620d, 0x661d, 0x6e3d, 0x441d, 0x4c3d, 0x5c7d, 
		0x083d, 0x187d, 0x38fd, 0x385f, 0x70a8, 0x7856, 0x70a4, 0x7853, 0x70a2, 0x70a1, 
		0x6168, 0x70b6, 0x6164, 0x70b3, 0x6162, 0x6161, 0x42e8, 0x6176, 0x42e4, 0x6173, 
		0x42e2, 0x42e1, 0x05e8, 0x42f6, 0x05e4, 0x42f3, 0x05e2, 0x05e1, 0x05f6, 0x05f3, 
		0x78d4, 0x7c6b, 0x0d7e, 0x78d2, 0x0d3f, 0x78d1, 0x7094, 0x784b, 0x71b4, 0x7092, 
		0x71b2, 0x7091, 0x71b1, 0x6134, 0x709b, 0x6374, 0x6132, 0x6372, 0x6131, 0x6371, 
		0x4274, 0x613b, 0x46f4, 0x4272, 0x46f2, 0x4271, 0x46f1, 0x04f4, 0x427b, 0x0df4, 
		0x04f2, 0x0df2, 0x04f1, 0x0df1, 0x04fb, 0x0dfb, 0x7cea, 0x1d7c, 0x4ebf, 0x7ce9, 
		0x1d3e, 0x1d1f, 0x78ca, 0x0cbf, 0x79da, 0x78c9, 0x1dbf, 0x79d9, 0x708a, 0x719a, 
		0x7089, 0x73ba, 0x7199, 0x73b9, 0x611a, 0x633a, 0x6119, 0x677a, 0x6339, 0x6779, 
		0x423a, 0x467a, 0x4239, 0x4efa, 0x4679, 0x4ef9, 0x047a, 0x0cfa, 0x0479, 0x1dfa, 
		0x0cf9, 0x1df9, 0x3d78, 0x5ebe, 0x3d3c, 0x5e9f, 0x3d1e, 0x3d0f, 0x7ce5, 0x1cbe, 
		0x7ded, 0x3dbe, 0x1c9f, 0x3d9f, 0x78c5, 0x79cd, 0x7bdd, 0x7085, 0x718d, 0x739d, 
		0x77bd, 0x610d, 0x631d, 0x673d, 0x6f7d, 0x421d, 0x463d, 0x4e7d, 0x5efd, 0x043d, 
		0x0c7d, 0x1cfd, 0x3cbc, 0x5e5f, 0x3c9e, 0x3c8f, 0x1c5f, 0x3cdf, 0x3c5e, 0x3c4f, 
		0x3c2f, 0x7054, 0x7052, 0x7051, 0x60b4, 0x705b, 0x60b2, 0x60b1, 0x4174, 0x60bb, 
		0x4172, 0x4171, 0x02f4, 0x417b, 0x02f2, 0x02f1, 0x02fb, 0x786a, 0x06bf, 0x7869, 
		0x704a, 0x70da, 0x7049, 0x70d9, 0x609a, 0x61ba, 0x6099, 0x61b9, 0x413a, 0x437a, 
		0x4139, 0x4379, 0x027a, 0x06fa, 0x0279, 0x06f9, 0x7c75, 0x0ebe, 0x0e9f, 0x7865, 
		0x78ed, 0x7045, 0x70cd, 0x71dd, 0x608d, 0x619d, 0x63bd, 0x411d, 0x433d, 0x477d, 
		0x023d, 0x067d, 0x0efd, 0x1ebc, 0x4f5f, 0x1e9e, 0x1e8f, 0x0e5f, 0x1edf, 0x3eb8, 
		0x5f5e, 0x3e9c, 0x5f4f, 0x3e8e, 0x3e87, 0x1e5e, 0x3ede, 0x1e4f, 0x3ecf, 0x3e5c, 
		0x5f2f, 0x3e4e, 0x3e47, 0x1e2f, 0x3e6f, 0x3e2e, 0x3e27, 0x3e17, 0x605a, 0x6059, 
		0x40ba, 0x40b9, 0x017a, 0x0179, 0x706d, 0x604d, 0x60dd, 0x409d, 0x41bd, 0x013d, 
		0x037d, 0x075f, 0x0f5e, 0x0f4f, 0x1f5c, 0x4faf, 0x1f4e, 0x1f47, 0x0f2f, 0x1f6f, 
		0x3f58, 0x5fae, 0x3f4c, 0x5fa7, 0x3f46, 0x3f43, 0x1f2e, 0x3f6e, 0x1f27, 0x3f67, 
		0x3f2c, 0x5f97, 0x3f26, 0x3f23, 0x1f17, 0x3f37, 0x3f16, 0x3f13, 0x07af, 0x0fae, 
		0x0fa7, 0x1fac, 0x4fd7, 0x1fa6, 0x1fa3, 0x0f97, 0x1fb7, 0x1f96, 0x1f93, 
		},
		{
		0x55f0, 0x6afc, 0x29e0, 0x54f8, 0x6a7e, 0x28f0, 0x547c, 0x6a3f, 0x2878, 0x543e, 
		0x283c, 0x7d68, 0x2df0, 0x56fc, 0x7d64, 0x2cf8, 0x567e, 0x7d62, 0x2c7c, 0x563f, 
		0x7d61, 0x2c3e, 0x7ae8, 0x7d76, 0x2efc, 0x7ae4, 0x7d73, 0x2e7e, 0x7ae2, 0x2e3f, 
		0x7ae1, 0x75e8, 0x7af6, 0x75e4, 0x7af3, 0x75e2, 0x75e1, 0x6be8, 0x75f6, 0x6be4, 
		0x75f3, 0x6be2, 0x6be1, 0x57e8, 0x6bf6, 0x57e4, 0x6bf3, 0x57e2, 0x25e0, 0x52f8, 
		0x697e, 0x24f0, 0x527c, 0x693f, 0x2478, 0x523e, 0x243c, 0x521f, 0x241e, 0x7d34, 
		0x26f8, 0x537e, 0x7d32, 0x267c, 0x533f, 0x7d31, 0x263e, 0x261f, 0x7a74, 0x7d3b, 
		0x277e, 0x7a72, 0x273f, 0x7a71, 0x74f4, 0x7a7b, 0x74f2, 0x74f1, 0x69f4, 0x74fb, 
		0x69f2, 0x69f1, 0x53f4, 0x69fb, 0x53f2, 0x53f1, 0x22f0, 0x517c, 0x68bf, 0x2278, 
		0x513e, 0x223c, 0x511f, 0x221e, 0x220f, 0x7d1a, 0x237c, 0x51bf, 0x7d19, 0x233e, 
		0x231f, 0x7a3a, 0x23bf, 0x7a39, 0x747a, 0x7479, 0x68fa, 0x68f9, 0x51fa, 0x51f9, 
		0x2178, 0x50be, 0x213c, 0x509f, 0x211e, 0x210f, 0x7d0d, 0x21be, 0x219f, 0x7a1d, 
		0x743d, 0x687d, 0x20bc, 0x505f, 0x209e, 0x208f, 0x20df, 0x205e, 0x204f, 0x15e0, 
		0x4af8, 0x657e, 0x14f0, 0x4a7c, 0x653f, 0x1478, 0x4a3e, 0x143c, 0x4a1f, 0x141e, 
		0x7cb4, 0x16f8, 0x4b7e, 0x7cb2, 0x167c, 0x4b3f, 0x7cb1, 0x163e, 0x161f, 0x7974, 
		0x7cbb, 0x177e, 0x7972, 0x173f, 0x7971, 0x72f4, 0x797b, 0x72f2, 0x72f1, 0x65f4, 
		0x72fb, 0x65f2, 0x65f1, 0x4bf4, 0x65fb, 0x4bf2, 0x4bf1, 0x5af0, 0x6d7c, 0x76bf, 
		0x34e0, 0x5a78, 0x6d3e, 0x3470, 0x5a3c, 0x6d1f, 0x3438, 0x5a1e, 0x341c, 0x5a0f, 
		0x340e, 0x12f0, 0x497c, 0x64bf, 0x36f0, 0x1278, 0x493e, 0x3678, 0x5b3e, 0x491f, 
		0x363c, 0x121e, 0x361e, 0x120f, 0x360f, 0x7c9a, 0x137c, 0x49bf, 0x7dba, 0x7c99, 
		0x377c, 0x133e, 0x7db9, 0x373e, 0x131f, 0x371f, 0x793a, 0x13bf, 0x7b7a, 0x7939, 
		0x37bf, 0x7b79, 0x727a, 0x76fa, 0x7279, 0x76f9, 0x64fa, 0x6dfa, 0x64f9, 0x6df9, 
		0x49fa, 0x49f9, 0x32e0, 0x5978, 0x6cbe, 0x3270, 0x593c, 0x6c9f, 0x3238, 0x591e, 
		0x321c, 0x590f, 0x320e, 0x3207, 0x1178, 0x48be, 0x3378, 0x113c, 0x489f, 0x333c, 
		0x599f, 0x331e, 0x110f, 0x330f, 0x7c8d, 0x11be, 0x7d9d, 0x33be, 0x119f, 0x339f, 
		0x791d, 0x7b3d, 0x723d, 0x767d, 0x647d, 0x6cfd, 0x48fd, 0x3170, 0x58bc, 0x6c5f, 
		0x3138, 0x589e, 0x311c, 0x588f, 0x310e, 0x3107, 0x10bc, 0x485f, 0x31bc, 0x109e, 
		0x319e, 0x108f, 0x318f, 0x10df, 0x31df, 0x30b8, 0x585e, 0x309c, 0x584f, 0x308e, 
		0x3087, 0x105e, 0x30de, 0x104f, 0x30cf, 0x305c, 0x582f, 0x304e, 0x3047, 0x102f, 
		0x306f, 0x302e, 0x3027, 0x0af0, 0x457c, 0x62bf, 0x0a78, 0x453e, 0x0a3c, 0x451f, 
		0x0a1e, 0x0a0f, 0x7c5a, 0x0b7c, 0x45bf, 0x7c59, 0x0b3e, 0x0b1f, 0x78ba, 0x0bbf, 
		0x78b9, 0x717a, 0x7179, 0x62fa, 0x62f9, 0x45fa, 0x45f9, 0x1ae0, 0x4d78, 0x66be, 
		0x1a70, 0x4d3c, 0x669f, 0x1a38, 0x4d1e, 0x1a1c, 0x4d0f, 0x1a0e, 0x1a07, 0x0978, 
		0x44be, 0x1b78, 0x093c, 0x449f, 0x1b3c, 0x4d9f, 0x1b1e, 0x090f, 0x1b0f, 0x7c4d, 
		0x09be, 0x7cdd, 0x1bbe, 0x099f, 0x1b9f, 0x789d, 0x79bd, 0x713d, 0x737d, 0x627d, 
		0x66fd, 0x44fd, 0x5d70, 0x6ebc, 0x775f, 0x3a60, 0x5d38, 0x6e9e, 0x3a30, 0x5d1c, 
		0x6e8f, 0x3a18, 0x5d0e, 0x3a0c, 0x5d07, 0x3a06, 0x1970, 0x4cbc, 0x665f, 0x3b70, 
		0x1938, 0x4c9e, 0x3b38, 0x5d9e, 0x4c8f, 0x3b1c, 0x190e, 0x3b0e, 0x1907, 0x3b07, 
		0x08bc, 0x445f, 0x19bc, 0x089e, 0x3bbc, 0x199e, 0x088f, 0x3b9e, 0x198f, 0x3b8f, 
		0x08df, 0x19df, 0x3bdf, 0x3960, 0x5cb8, 0x6e5e, 0x3930, 0x5c9c, 0x6e4f, 0x3918, 
		0x5c8e, 0x390c, 0x5c87, 0x3906, 0x3903, 0x18b8, 0x4c5e, 0x39b8, 0x189c, 0x4c4f, 
		0x399c, 0x5ccf, 0x398e, 0x1887, 0x3987, 0x085e, 0x18de, 0x084f, 0x39de, 0x18cf, 
		0x39cf, 0x38b0, 0x5c5c, 0x6e2f, 0x3898, 0x5c4e, 0x388c, 0x5c47, 0x3886, 0x3883, 
		0x185c, 0x4c2f, 0x38dc, 0x184e, 0x38ce, 0x1847, 0x38c7, 0x082f, 0x186f, 0x38ef, 
		0x3858, 0x5c2e, 0x384c, 0x5c27, 0x3846, 0x3843, 0x182e, 0x386e, 0x1827, 0x3867, 
		0x382c, 0x5c17, 0x3826, 0x3823, 0x1817, 0x3837, 0x3816, 0x3813, 0x0578, 0x42be, 
		0x053c, 0x429f, 0x051e, 0x050f, 0x05be, 0x059f, 0x785d, 0x70bd, 0x617d, 0x42fd, 
		0x0d70, 0x46bc, 0x635f, 0x0d38, 0x469e, 0x0d1c, 0x468f, 0x0d0e, 0x0d07, 0x04bc, 
		0x425f, 0x0dbc, 0x049e, 0x0d9e, 0x048f, 0x0d8f, 0x04df, 0x0ddf, 0x1d60, 0x4eb8, 
		0x675e, 0x1d30, 0x4e9c, 0x674f, 0x1d18, 0x4e8e, 0x1d0c, 0x4e87, 0x1d06, 0x1d03, 
		0x0cb8, 0x465e, 0x1db8, 0x0c9c, 0x464f, 0x1d9c, 0x0c8e, 0x1d8e, 0x0c87, 0x1d87, 
		0x045e, 0x0cde, 0x044f, 0x1dde, 0x0ccf, 0x1dcf, 0x5eb0, 0x6f5c, 0x77af, 0x3d20, 
		0x5e98, 0x6f4e, 0x3d10, 0x5e8c, 0x6f47, 0x3d08, 0x5e86, 0x3d04, 0x5e83, 0x3d02, 
		0x1cb0, 0x4e5c, 0x672f, 0x3db0, 0x1c98, 0x4e4e, 0x3d98, 0x5ece, 0x4e47, 0x3d8c, 
		0x1c86, 0x3d86, 0x1c83, 0x3d83, 0x0c5c, 0x462f, 0x1cdc, 0x0c4e, 0x3ddc, 0x1cce, 
		0x0c47, 0x3dce, 0x1cc7, 0x3dc7, 0x042f, 0x0c6f, 0x1cef, 0x3def, 0x3ca0, 0x5e58, 
		0x6f2e, 0x3c90, 0x5e4c, 0x6f27, 0x3c88, 0x5e46, 0x3c84, 0x5e43, 0x3c82, 0x3c81, 
		0x1c58, 0x4e2e, 0x3cd8, 0x1c4c, 0x4e27, 0x3ccc, 0x5e67, 0x3cc6, 0x1c43, 0x3cc3, 
		0x0c2e, 0x1c6e, 0x0c27, 0x3cee, 0x1c67, 0x3ce7, 0x3c50, 0x5e2c, 0x6f17, 0x3c48, 
		0x5e26, 0x3c44, 0x5e23, 0x3c42, 0x3c41, 0x1c2c, 0x4e17, 0x3c6c, 0x1c26, 0x3c66, 
		0x1c23, 0x3c63, 0x0c17, 0x1c37, 0x3c77, 0x3c28, 0x5e16, 0x3c24, 0x5e13, 0x3c22, 
		0x3c21, 0x1c16, 0x3c36, 0x1c13, 0x3c33, 0x3c14, 0x5e0b, 0x3c12, 0x3c11, 0x1c0b, 
		0x3c1b, 0x02bc, 0x415f, 0x029e, 0x028f, 0x02df, 0x06b8, 0x435e, 0x069c, 0x434f, 
		0x068e, 0x0687, 0x025e, 0x06de, 0x024f, 0x06cf, 0x0eb0, 0x475c, 0x63af, 0x0e98, 
		0x474e, 0x0e8c, 0x4747, 0x0e86, 0x0e83, 0x065c, 0x432f, 0x0edc, 0x064e, 0x0ece, 
		0x0647, 0x0ec7, 0x022f, 0x066f, 0x0eef, 0x1ea0, 0x4f58, 0x67ae, 0x1e90, 0x4f4c, 
		0x67a7, 0x1e88, 0x4f46, 0x1e84, 0x4f43, 0x1e82, 0x1e81, 0x0e58, 0x472e, 0x1ed8, 
		0x0e4c, 0x4727, 0x1ecc, 0x4f67, 0x1ec6, 0x0e43, 0x1ec3, 0x062e, 0x0e6e, 0x0627, 
		0x1eee, 0x0e67, 0x1ee7, 0x5f50, 0x6fac, 0x77d7, 0x5f48, 0x6fa6, 0x5f44, 0x6fa3, 
		0x5f42, 0x5f41, 0x1e50, 0x4f2c, 0x6797, 0x3ed0, 0x1e48, 0x4f26, 0x3ec8, 0x5f66, 
		0x4f23, 0x3ec4, 0x1e42, 0x3ec2, 0x1e41, 0x3ec1, 0x0e2c, 0x4717, 0x1e6c, 0x0e26, 
		0x3eec, 0x1e66, 0x0e23, 0x3ee6, 0x1e63, 0x3ee3, 0x0617, 0x0e37, 0x1e77, 0x3ef7, 
		0x5f28, 0x6f96, 0x5f24, 0x6f93, 0x5f22, 0x5f21, 0x1e28, 0x4f16, 0x3e68, 0x1e24, 
		0x4f13, 0x3e64, 0x5f33, 0x3e62, 0x1e21, 0x3e61, 0x0e16, 0x1e36, 0x0e13, 0x3e76, 
		0x1e33, 0x3e73, 0x5f14, 0x6f8b, 0x5f12, 0x5f11, 0x1e14, 0x4f0b, 0x3e34, 0x1e12, 
		0x3e32, 0x1e11, 0x3e31, 0x0e0b, 0x1e1b, 0x3e3b, 0x5f0a, 0x5f09, 0x1e0a, 0x3e1a, 
		0x1e09, 0x3e19, 0x015e, 0x014f, 0x035c, 0x41af, 0x034e, 0x0347, 0x012f, 0x036f, 
		0x0758, 0x43ae, 0x074c, 0x43a7, 0x0746, 0x0743, 0x032e, 0x076e, 0x0327, 0x0767, 
		0x0f50, 0x47ac, 0x63d7, 0x0f48, 0x47a6, 0x0f44, 0x47a3, 0x0f42, 0x0f41, 0x072c, 
		0x4397, 0x0f6c, 0x47b7, 0x0f66, 0x0723, 0x0f63, 0x0317, 0x0737, 0x0f77, 0x4fa8, 
		0x67d6, 0x4fa4, 0x67d3, 0x4fa2, 0x4fa1, 0x0f28, 0x4796, 0x1f68, 0x4fb6, 0x4793, 
		0x1f64, 0x0f22, 0x1f62, 0x0f21, 0x1f61, 0x0716, 0x0f36, 0x0713, 0x1f76, 0x0f33, 
		0x1f73, 0x6fd4, 0x77eb, 0x6fd2, 0x6fd1, 0x4f94, 0x67cb, 0x5fb4, 0x4f92, 0x5fb2, 
		0x4f91, 0x5fb1, 0x0f14, 0x478b, 0x1f34, 0x0f12, 0x3f74, 0x1f32, 0x0f11, 0x3f72, 
		0x1f31, 0x3f71, 0x070b, 0x0f1b, 0x1f3b, 0x3f7b, 0x6fca, 0x6fc9, 0x4f8a, 0x5f9a, 
		0x4f89, 0x5f99, 0x0f0a, 0x1f1a, 0x0f09, 0x3f3a, 0x1f19, 0x3f39, 0x6fc5, 0x4f85, 
		0x5f8d, 0x0f05, 0x1f0d, 0x3f1d, 0x01ae, 0x01a7, 0x03ac, 0x41d7, 0x03a6, 0x03a3, 
		0x0197, 0x03b7, 0x07a8, 0x43d6, 0x07a4, 0x43d3, 0x07a2, 0x07a1, 0x0396, 0x07b6, 
		0x0393, 0x07b3, 0x47d4, 0x63eb, 0x47d2, 0x47d1, 0x0794, 0x43cb, 0x0fb4, 0x47db, 
		0x0fb2, 0x0791, 0x0fb1, 0x038b, 0x079b, 0x0fbb, 0x67ea, 0x67e9, 0x47ca, 0x4fda, 
		0x47c9, 0x4fd9, 0x078a, 0x0f9a, 0x0789, 0x1fba, 0x0f99, 0x1fb9, 0x67e5, 0x47c5, 
		0x4fcd, 0x0785, 0x0f8d, 0x1f9d, 0x01d6, 0x01d3, 0x03d4, 0x41eb, 0x03d2, 0x03d1, 
		0x01cb, 0x03db, 0x43ea, 0x43e9, 0x03ca, 0x07da, 0x03c9, 0x07d9, 0x63f5, 
		},
		};
	}
}
