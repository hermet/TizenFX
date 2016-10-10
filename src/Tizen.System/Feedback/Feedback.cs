using System;
using System.Collections.Generic;


namespace Tizen.System
{
    /// <summary>
    /// Class for constants
    /// </summary>
    internal static class Constants
    {
        internal const int NumberOfPattern = 39;
    }

    /// <summary>
    /// The Feedback API provides functions to control haptic and sound.
    /// The Feedback API provides the way to play and stop feedback and get the information whether specific pattern is supported.
    /// Below is supported pattern string.
    /// Tap
    /// SoftInputPanel
    /// Key0
    /// Key1
    /// Key2
    /// Key3
    /// Key4
    /// Key5
    /// Key6
    /// Key7
    /// Key8
    /// Key9
    /// KeyStar
    /// KeySharp
    /// KeyBack
    /// Hold
    /// HardwareKeyPressed
    /// HardwareKeyHold
    /// Message
    /// Email
    /// WakeUp
    /// Schedule
    /// Timer
    /// General
    /// PowerOn
    /// PowerOff
    /// ChargerConnected
    /// ChargingError
    /// FullyCharged
    /// LowBattery
    /// Lock
    /// UnLock
    /// VibrationModeAbled
    /// SilentModeDisabled
    /// BluetoothDeviceConnected
    /// BluetoothDeviceDisconnected
    /// ListReorder
    /// ListSlider
    /// VolumeKeyPressed
    /// </summary>
    /// <privilege>
    /// For controlling haptic device:
    /// http://tizen.org/privilege/haptic
    /// For controlling sound, previlege is not needed.
    /// </privilege>
    /// <code>
    /// Feedback feedback = new Feedback();
    /// bool res = feedback.IsSupportedPattern(FeedbackType.Vibration, "Tap");
    /// </code>
    public class Feedback
    {
        private const string LogTag = "Tizen.System.Feedback";

        private readonly FeedbackPattern[] Pattern = new FeedbackPattern[39];
        public Feedback()
        {
            Pattern[0].PatternNumber = 0;
            Pattern[0].PatternString = "Tap";
            Pattern[1].PatternNumber = 1;
            Pattern[1].PatternString = "SoftInputPanel";
            Pattern[2].PatternNumber = 6;
            Pattern[2].PatternString = "Key0";
            Pattern[3].PatternNumber = 7;
            Pattern[3].PatternString = "Key1";
            Pattern[4].PatternNumber = 8;
            Pattern[4].PatternString = "Key2";
            Pattern[5].PatternNumber = 9;
            Pattern[5].PatternString = "Key3";
            Pattern[6].PatternNumber = 10;
            Pattern[6].PatternString = "Key4";
            Pattern[7].PatternNumber = 11;
            Pattern[7].PatternString = "Key5";
            Pattern[8].PatternNumber = 12;
            Pattern[8].PatternString = "Key6";
            Pattern[9].PatternNumber = 13;
            Pattern[9].PatternString = "Key7";
            Pattern[10].PatternNumber = 14;
            Pattern[10].PatternString = "Key8";
            Pattern[11].PatternNumber = 15;
            Pattern[11].PatternString = "Key9";
            Pattern[12].PatternNumber = 16;
            Pattern[12].PatternString = "KeyStar";
            Pattern[13].PatternNumber = 17;
            Pattern[13].PatternString = "KeySharp";
            Pattern[14].PatternNumber = 18;
            Pattern[14].PatternString = "KeyBack";
            Pattern[15].PatternNumber = 19;
            Pattern[15].PatternString = "Hold";
            Pattern[16].PatternNumber = 21;
            Pattern[16].PatternString = "HardwareKeyPressed";
            Pattern[17].PatternNumber = 22;
            Pattern[17].PatternString = "HardwareKeyHold";
            Pattern[18].PatternNumber = 23;
            Pattern[18].PatternString = "Message";
            Pattern[19].PatternNumber = 25;
            Pattern[19].PatternString = "Email";
            Pattern[20].PatternNumber = 27;
            Pattern[20].PatternString = "WakeUp";
            Pattern[21].PatternNumber = 29;
            Pattern[21].PatternString = "Schedule";
            Pattern[22].PatternNumber = 31;
            Pattern[22].PatternString = "Timer";
            Pattern[23].PatternNumber = 33;
            Pattern[23].PatternString = "General";
            Pattern[24].PatternNumber = 36;
            Pattern[24].PatternString = "PowerOn";
            Pattern[25].PatternNumber = 37;
            Pattern[25].PatternString = "PowerOff";
            Pattern[26].PatternNumber = 38;
            Pattern[26].PatternString = "ChargerConnected";
            Pattern[27].PatternNumber = 40;
            Pattern[27].PatternString = "ChargingError";
            Pattern[28].PatternNumber = 42;
            Pattern[28].PatternString = "FullyCharged";
            Pattern[29].PatternNumber = 44;
            Pattern[29].PatternString = "LowBattery";
            Pattern[30].PatternNumber = 46;
            Pattern[30].PatternString = "Lock";
            Pattern[31].PatternNumber = 47;
            Pattern[31].PatternString = "UnLock";
            Pattern[32].PatternNumber = 55;
            Pattern[32].PatternString = "VibrationModeAbled";
            Pattern[33].PatternNumber = 56;
            Pattern[33].PatternString = "SilentModeDisabled";
            Pattern[34].PatternNumber = 57;
            Pattern[34].PatternString = "BluetoothDeviceConnected";
            Pattern[35].PatternNumber = 58;
            Pattern[35].PatternString = "BluetoothDeviceDisconnected";
            Pattern[36].PatternNumber = 62;
            Pattern[36].PatternString = "ListReorder";
            Pattern[37].PatternNumber = 63;
            Pattern[37].PatternString = "ListSlider";
            Pattern[38].PatternNumber = 64;
            Pattern[38].PatternString = "VolumeKeyPressed";

            Interop.Feedback.FeedbackError res = (Interop.Feedback.FeedbackError)Interop.Feedback.Initialize();
            if (res != Interop.Feedback.FeedbackError.None)
            {
                Log.Warn(LogTag, string.Format("Failed to initialize feedback. err = {0}", res));
                switch (res)
                {
                    case Interop.Feedback.FeedbackError.NotSupported:
                        throw new NotSupportedException("Device is not supported");
                    default:
                        throw new InvalidOperationException("Failed to initialize");
                }
            }
        }

        ~Feedback()
        {
            Interop.Feedback.FeedbackError res = (Interop.Feedback.FeedbackError)Interop.Feedback.Deinitialize();
            if (res != Interop.Feedback.FeedbackError.None)
            {
                Log.Warn(LogTag, string.Format("Failed to deinitialize feedback. err = {0}", res));
                switch (res)
                {
                    case Interop.Feedback.FeedbackError.NotInitialized:
                        throw new Exception("Not initialized");
                    default:
                        throw new InvalidOperationException("Failed to initialize");
                }
            }
        }

        /// <summary>
        /// Get supported information about specific type and pattern
        /// </summary>
        /// <remarks>
        /// Now, IsSupportedPattern is not working for FeedbackType.All.
        /// This API is working for FeedbackType.Sound and FeedbackType.Vibration only.
        /// If you use FeedbackType.All for type parameter, this API will throw ArgumentException.
        /// To get supported information for Vibration type, app should have http://tizen.org/privilege/haptic privilege.
        /// </remarks>
        /// <param name="type">Feedback type</param>
        /// <param name="pattern">Feedback pattern string</param>
        /// <returns>Information whether pattern is supported</returns>
        /// <exception cref="Exception">Thrown when failed because feedback is not initialized</exception>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid arguament</exception>
        /// <exception cref="NotSupportedException">Thrown when failed becuase device(haptic, sound) is not supported</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when failed because access is not granted(No privilege)</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of system error</exception>
        /// <privilege>http://tizen.org/privilege/haptic</privilege>
        /// <example>
        /// <code>
	    /// Feedback feedback = new Feedback();
        /// bool res = feedback.IsSupportedPattern(FeedbackType.Vibration, "Tap");
        /// </code>
        /// </example>
        public bool IsSupportedPattern(FeedbackType type, String pattern)
        {
            bool supported = false;
            bool found = false;
            int i = 0;
            Interop.Feedback.FeedbackError res;

            for (i = 0; i < Constants.NumberOfPattern; i++)
            {
                if (String.Compare(pattern, Pattern[i].PatternString) == 0)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new ArgumentException("Invalid Arguments");

            res = (Interop.Feedback.FeedbackError)Interop.Feedback.IsSupportedPattern((Interop.Feedback.FeedbackType)type, Pattern[i].PatternNumber, out supported);


            if (res != Interop.Feedback.FeedbackError.None)
            {
                Log.Warn(LogTag, string.Format("Failed to get supported information. err = {0}", res));
                switch (res)
                {
                    case Interop.Feedback.FeedbackError.NotInitialized:
                        throw new Exception("Not initialized");
                    case Interop.Feedback.FeedbackError.InvalidParameter:
                        throw new ArgumentException("Invalid Arguments");
                    case Interop.Feedback.FeedbackError.NotSupported:
                        throw new NotSupportedException("Device is not supported");
                    case Interop.Feedback.FeedbackError.PermissionDenied:
                        throw new UnauthorizedAccessException("Access is not granted");
                    case Interop.Feedback.FeedbackError.OperationFailed:
                    default:
                        throw new InvalidOperationException("Failed to get supported information");
                }
            }
            return supported;
        }

        /// <summary>
        /// Play specific feedback pattern
        /// </summary>
        /// <remarks>
        /// To play Vibration type, app should have http://tizen.org/privilege/haptic privilege.
        /// </remarks>
        /// <param name="type">Feedback type</param>
        /// <param name="pattern">Feedback pattern string</param>
        /// <exception cref="Exception">Thrown when failed because feedback is not initialized</exception>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid arguament</exception>
        /// <exception cref="NotSupportedException">Thrown when failed because device(haptic, sound) or specific pattern is not supported</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when failed because access is not granted(No privilege)</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of system error</exception>
        /// <privilege>http://tizen.org/privilege/haptic</privilege>
        /// <example>
        /// <code>
        /// Feedback feedback = new Feedback();
        /// feedback.Play(FeedbackType.All, "Tap");
        /// </code>
        /// </example>
        public void Play(FeedbackType type, String pattern)
        {
            bool found = false;
            int i = 0;
            Interop.Feedback.FeedbackError res;

            for (i = 0; i < Constants.NumberOfPattern; i++)
            {
                if (String.Compare(pattern, Pattern[i].PatternString) == 0)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new ArgumentException("Invalid Arguments");

            if (type == FeedbackType.All)
                res = (Interop.Feedback.FeedbackError)Interop.Feedback.Play(Pattern[i].PatternNumber);
            else
                res = (Interop.Feedback.FeedbackError)Interop.Feedback.PlayType((Interop.Feedback.FeedbackType)type, Pattern[i].PatternNumber);

            if (res != Interop.Feedback.FeedbackError.None)
            {
                Log.Warn(LogTag, string.Format("Failed to play feedback. err = {0}", res));
                switch (res)
                {
                    case Interop.Feedback.FeedbackError.NotInitialized:
                        throw new Exception("Not initialized");
                    case Interop.Feedback.FeedbackError.InvalidParameter:
                        throw new ArgumentException("Invalid Arguments");
                    case Interop.Feedback.FeedbackError.NotSupported:
                        throw new NotSupportedException("Not supported");
                    case Interop.Feedback.FeedbackError.PermissionDenied:
                        throw new UnauthorizedAccessException("Access is not granted");
                    case Interop.Feedback.FeedbackError.OperationFailed:
                    default:
                        throw new InvalidOperationException("Failed to play pattern");
                }
            }
        }

        /// <summary>
        /// Stop to play feedback
        /// </summary>
        /// <remarks>
        /// To stop vibration, app should have http://tizen.org/privilege/haptic privilege.
        /// </remarks>
        /// <exception cref="Exception">Thrown when failed because feedback is not initialized</exception>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid arguament</exception>
        /// <exception cref="NotSupportedException">Thrown when failed because device(haptic, sound) or specific pattern is not supported</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when failed because access is not granted(No privilege)</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of system error</exception>
        /// <privilege>http://tizen.org/privilege/haptic</privilege>
        /// <example>
        /// <code>
        /// Feedback Feedback1 = new Feedback();
        /// Feedback1.Stop();
        /// </code>
        /// </example>
        public void Stop()
        {
            Interop.Feedback.FeedbackError res = (Interop.Feedback.FeedbackError)Interop.Feedback.Stop();

            if (res != Interop.Feedback.FeedbackError.None)
            {
                Log.Warn(LogTag, string.Format("Failed to stop feedback. err = {0}", res));
                switch (res)
                {
                    case Interop.Feedback.FeedbackError.NotInitialized:
                        throw new Exception("Not initialized");
                    case Interop.Feedback.FeedbackError.InvalidParameter:
                        throw new ArgumentException("Invalid Arguments");
                    case Interop.Feedback.FeedbackError.NotSupported:
                        throw new NotSupportedException("Not supported");
                    case Interop.Feedback.FeedbackError.PermissionDenied:
                        throw new UnauthorizedAccessException("Access is not granted");
                    case Interop.Feedback.FeedbackError.OperationFailed:
                    default:
                        throw new InvalidOperationException("Failed to play pattern");
                }
            }
        }
    }
}
