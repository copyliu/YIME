using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RimeApi
{
    public static class Rime
    {
        /// <summary>
        ///  Call this function before accessing any other API.
        /// </summary>
        /// <param name="traits"></param>
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeSetup(ref RimeTraits traits);

        /// <summary>
        ///  *  Pass a C-string constant in the format "rime.x"
        ///  *  where 'x' is the name of your application.
        ///  *  Add prefix "rime." to ensure old log files are automatically cleaned.
        ///  *  \deprecated Use RimeSetup() instead.
        /// </summary>
        /// <param name="app_name"></param>
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeSetupLogging(
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            string app_name);



        /// <summary>
        /// //! Receive notifications
        /// /*!
        ///  * - on loading schema:
        ///  *   + message_type="schema", message_value="luna_pinyin/Luna Pinyin"
        ///  * - on changing mode:
        ///  *   + message_type="option", message_value="ascii_mode"
        ///  *   + message_type="option", message_value="!ascii_mode"
        ///  * - on deployment:
        ///  *   + session_id = 0, message_type="deploy", message_value="start"
        ///  *   + session_id = 0, message_type="deploy", message_value="success"
        ///  *   + session_id = 0, message_type="deploy", message_value="failure"
        ///  *
        ///  *   handler will be called with context_object as the first parameter
        ///  *   every time an event occurs in librime, until RimeFinalize() is called.
        ///  *   when handler is NULL, notification is disabled.
        ///  */
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="context_object"></param>
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeSetNotificationHandler(
            RimeNotificationHandler handler,
            IntPtr context_object);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RimeNotificationHandler(IntPtr context_object, 
                                                    UIntPtr session_id,
                                                    IntPtr message_type,
                                                    IntPtr message_value);
        // Entry and exit
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeInitialize(ref   RimeTraits traits);
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeInitialize(IntPtr traits);
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeFinalize();

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeStartMaintenance(bool full_check);
        //! \deprecated Use RimeStartMaintenance(full_check = False) instead.
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeStartMaintenanceOnWorkspaceChange();
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeIsMaintenancing();
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeJoinMaintenanceThread();

        // Deployment
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeDeployerInitialize(ref   RimeTraits traits);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimePrebuildAllSchemas();

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeDeployWorkspace();

        [DllImport("rime.dll",CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeDeploySchema(
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            string schema_file);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeDeployConfigFile(
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            string file_name,
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            string version_key);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeSyncUserData();


        // Session management

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr RimeCreateSession();

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeFindSession(UIntPtr session_id);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeDestroySession(UIntPtr session_id);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeCleanupStaleSessions();

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeCleanupAllSessions();

        // Input
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeProcessKey(UIntPtr session_id, int keycode, int mask);


        /*!
         * return True if there is unread commit text
         */
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeCommitComposition(UIntPtr session_id);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeClearComposition(UIntPtr session_id);

        // Output
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeGetCommit(UIntPtr session_id, ref   RimeCommit commit);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeFreeCommit(ref   RimeCommit commit);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeGetContext(UIntPtr session_id,
            ref   RimeContext context);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeFreeContext(ref   RimeContext context);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeGetStatus(UIntPtr session_id,
            ref   RimeStatus status);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeFreeStatus(ref   RimeStatus status);


        // Accessing candidate list
        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeCandidateListBegin(UIntPtr session_id,
            ref   RimeCandidateListIterator iterator);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeCandidateListNext(
            ref    RimeCandidateListIterator iterator);

        [DllImport("rime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RimeCandidateListEnd(
            ref    RimeCandidateListIterator iterator);



        //
        //// Runtime options
        //
        //RIME_API void RimeSetOption(RimeSessionId session_id, const char* option, Bool value);
        //RIME_API Bool RimeGetOption(RimeSessionId session_id, const char* option);
        //
        //RIME_API void RimeSetProperty(RimeSessionId session_id, const char* prop, const char* value);
        //RIME_API Bool RimeGetProperty(RimeSessionId session_id, const char* prop, char* value, size_t buffer_size);
        //
        //RIME_API Bool RimeGetSchemaList(RimeSchemaList* schema_list);
        //RIME_API void RimeFreeSchemaList(RimeSchemaList* schema_list);
        //RIME_API Bool RimeGetCurrentSchema(RimeSessionId session_id, char* schema_id, size_t buffer_size);
        //RIME_API Bool RimeSelectSchema(RimeSessionId session_id, const char* schema_id);
        //
        //// Configuration
        //
        //// <schema_id>.schema.yaml
        //RIME_API Bool RimeSchemaOpen(const char* schema_id, RimeConfig* config);
        //// <config_id>.yaml
        //RIME_API Bool RimeConfigOpen(const char* config_id, RimeConfig* config);
        //RIME_API Bool RimeConfigClose(RimeConfig* config);
        //RIME_API Bool RimeConfigInit(RimeConfig* config);
        //RIME_API Bool RimeConfigLoadString(RimeConfig* config, const char* yaml);
        //// Access config values
        //RIME_API Bool RimeConfigGetBool(RimeConfig* config, const char* key, Bool *value);
        //RIME_API Bool RimeConfigGetInt(RimeConfig* config, const char* key, int* value);
        //RIME_API Bool RimeConfigGetDouble(RimeConfig* config, const char* key, double* value);
        //RIME_API Bool RimeConfigGetString(RimeConfig* config, const char* key,
        //            char* value, size_t buffer_size);
        //RIME_API const char* RimeConfigGetCString(RimeConfig * config, const char* key);
        //RIME_API Bool RimeConfigSetBool(RimeConfig* config, const char* key, Bool value);
        //RIME_API Bool RimeConfigSetInt(RimeConfig* config, const char* key, int value);
        //RIME_API Bool RimeConfigSetDouble(RimeConfig* config, const char* key, double value);
        //RIME_API Bool RimeConfigSetString(RimeConfig* config, const char* key, const char* value);
        //// Manipulate complex structures
        //RIME_API Bool RimeConfigGetItem(RimeConfig* config, const char* key, RimeConfig* value);
        //RIME_API Bool RimeConfigSetItem(RimeConfig* config, const char* key, RimeConfig* value);
        //RIME_API Bool RimeConfigClear(RimeConfig* config, const char* key);
        //RIME_API Bool RimeConfigCreateList(RimeConfig* config, const char* key);
        //RIME_API Bool RimeConfigCreateMap(RimeConfig* config, const char* key);
        //RIME_API size_t RimeConfigListSize(RimeConfig* config, const char* key);
        //RIME_API Bool RimeConfigBeginList(RimeConfigIterator* iterator, RimeConfig* config, const char* key);
        //RIME_API Bool RimeConfigBeginMap(RimeConfigIterator* iterator, RimeConfig* config, const char* key);
        //RIME_API Bool RimeConfigNext(RimeConfigIterator* iterator);
        //RIME_API void RimeConfigEnd(RimeConfigIterator* iterator);
        //// Utilities
        //RIME_API Bool RimeConfigUpdateSignature(RimeConfig* config, const char* signer);
        //
        //// Testing
        //
        [DllImport("rime.dll",CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RimeSimulateKeySequence(UIntPtr session_id, [MarshalAs(UnmanagedType.LPUTF8Str)] string key_sequence);


    }
}
