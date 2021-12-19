using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibPinyinApi
{
    [Flags]
    public  enum PinyinOption:uint
    {
        IS_PINYIN = 1U << 1,
        IS_ZHUYIN = 1U << 2,
        PINYIN_INCOMPLETE = 1U << 3,
        ZHUYIN_INCOMPLETE = 1U << 4,
        USE_TONE = 1U << 5,
        FORCE_TONE = 1U << 6,
        USE_DIVIDED_TABLE = 1U << 7,
        USE_RESPLIT_TABLE = 1U << 8,
        DYNAMIC_ADJUST = 1U << 9,
        PINYIN_AMB_C_CH = 1U << 10,
        PINYIN_AMB_S_SH = 1U << 11,
        PINYIN_AMB_Z_ZH = 1U << 12,
        PINYIN_AMB_F_H = 1U << 13,
        PINYIN_AMB_G_K = 1U << 14,
        PINYIN_AMB_L_N = 1U << 15,
        PINYIN_AMB_L_R = 1U << 16,
        PINYIN_AMB_AN_ANG = 1U << 17,
        PINYIN_AMB_EN_ENG = 1U << 18,
        PINYIN_AMB_IN_ING = 1U << 19,
        PINYIN_AMB_ALL = 0x3FFU << 10,
        PINYIN_CORRECT_GN_NG = 1U << 21,
        PINYIN_CORRECT_MG_NG = 1U << 22,
        PINYIN_CORRECT_IOU_IU = 1U << 23,
        PINYIN_CORRECT_UEI_UI = 1U << 24,
        PINYIN_CORRECT_UEN_UN = 1U << 25,
        PINYIN_CORRECT_UE_VE = 1U << 26,
        PINYIN_CORRECT_V_U = 1U << 27,
        PINYIN_CORRECT_ON_ONG = 1U << 28,
        PINYIN_CORRECT_ALL = 0xFFU << 21,

    }

    internal static class LibPinyinDll
    {
        /// <summary>
        /// Create a new pinyin context.
        /// </summary>
        /// <param name="systemdir">the system wide language model data directory.</param>
        /// <param name="userdir">the user's language model data directory.</param>
        /// <returns>the newly created pinyin context, NULL if failed.</returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pinyin_init([MarshalAs(UnmanagedType.LPUTF8Str)] string systemdir,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string userdir);

        /// <summary>
        /// Save the user's self-learning information of the pinyin context.
        /// </summary>
        /// <param name="context">the pinyin context to be saved into user directory.</param>
        /// <returns>whether the save succeeded.</returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_save(IntPtr context);
        /// <summary>
        /// Finalize the pinyin context.
        /// </summary>
        /// <param name="context">the pinyin context.</param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_fini(IntPtr context);
        /// <summary>
        /// Mask out the matched phrase tokens.
        /// </summary>
        /// <param name="context">the pinyin context.</param>
        /// <param name="mask"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_mask_out(IntPtr context, UInt32 mask, UInt32 value);
        /// <summary>
        /// Set the options of the pinyin context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_set_options(IntPtr context, [MarshalAs(UnmanagedType.U4)] PinyinOption option);
        /// <summary>
        /// Allocate a new pinyin instance from the context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pinyin_alloc_instance(IntPtr context);
        /// <summary>
        /// Free the pinyin instance.
        /// </summary>
        /// <param name="instance"></param>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void pinyin_free_instance(IntPtr instance);
        /// <summary>
        /// Get the pinyin context from the pinyin instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pinyin_get_context(IntPtr instance);
        /// <summary>
        /// Guess a sentence from the saved pinyin keys in the instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_guess_sentence(IntPtr instance);
        /// <summary>
        ///  Guess a sentence from the saved pinyin keys with a prefix.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_guess_sentence_with_prefix(IntPtr instance,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string prefix);
        /// <summary>
        /// Segment a sentence and saved the result in the instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_phrase_segment(IntPtr instance,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string sentence);
        /// <summary>
        /// Get the sentence from the instance.
        /// Note: the returned sentence should be freed by g_free().
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_sentence(IntPtr instance, UInt32 index, ref IntPtr sentence);

        [DllImport("libglib-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void g_free(IntPtr pointer);
        /// <summary>
        /// Parse multiple full pinyins and save it in the instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="pinyins"></param>
        /// <returns>the parsed length of the full pinyins.</returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int pinyin_parse_more_full_pinyins(IntPtr instance,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pinyins);
        /// <summary>
        /// Guess the candidates at the offset.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="offset"></param>
        /// <param name="sort_option"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_guess_candidates(IntPtr instance, int offset, int sort_option);
        /// <summary>
        /// Train the current user input sentence.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_train(IntPtr instance, int index);
        /// <summary>
        /// Reset the pinyin instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_reset(IntPtr instance);
        /// <summary>
        /// Get the number of the candidates.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_n_candidate(IntPtr instance, ref int num);
        /// <summary>
        /// Get the candidate of the index from the candidates.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="index"></param>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_candidate(IntPtr instance, int index, ref IntPtr candidate);
        /// <summary>
        ///  Get the string of the candidate.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="candidate"></param>
        /// <param name="utf8_str"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_candidate_string(IntPtr instance, IntPtr candidate, ref IntPtr utf8_str);
        /// <summary>
        ///  Choose a full pinyin candidate at the offset.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="offset"></param>
        /// <param name="candidate">the selected candidate.</param>
        /// <returns>the cursor after the chosen candidate.</returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int pinyin_choose_candidate(IntPtr instance, int offset, IntPtr candidate);
        /// <summary>
        /// Get the number of the phrase tokens.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_n_phrase(IntPtr instance, ref int num);
        /// <summary>
        ///  Get the phrase token of the index from the phrase tokens.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="index"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_phrase_token(IntPtr instance, int index, ref IntPtr token);
        /// <summary>
        /// Get the auxiliary text for full pinyin.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cursor"></param>
        /// <param name="aux_text"></param>
        /// <returns></returns>
        [DllImport("pinyin.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool pinyin_get_full_pinyin_auxiliary_text(IntPtr instance, int cursor,
            ref IntPtr aux_text);
    }
}
