using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.CameraController
{
    /// <summary>
    /// Generates events and processes commands for the CameraController device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the CameraController device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="CameraController"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1168;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(CameraController);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(CameraStart) },
            { 33, typeof(CameraStop) },
            { 34, typeof(ServoEnable) },
            { 35, typeof(ServoDisable) },
            { 36, typeof(OutputSet) },
            { 37, typeof(OutputClear) },
            { 38, typeof(OutputState) },
            { 39, typeof(DigitalInputs) },
            { 40, typeof(Camera0Trigger) },
            { 41, typeof(Camera1Trigger) },
            { 42, typeof(Camera0Sync) },
            { 43, typeof(Camera1Sync) },
            { 44, typeof(ServoState) },
            { 46, typeof(SyncInterval) },
            { 48, typeof(DI0Mode) },
            { 49, typeof(Control0Mode) },
            { 50, typeof(Camera0Frequency) },
            { 51, typeof(Servo0Period) },
            { 52, typeof(Servo0PulseWidth) },
            { 53, typeof(Control1Mode) },
            { 54, typeof(Camera1Frequency) },
            { 55, typeof(Servo1Period) },
            { 56, typeof(Servo1PulseWidth) },
            { 59, typeof(EnableEvents) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="CameraController"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of CameraController messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="CameraController"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="CameraController"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="CameraController"/> device.
    /// </summary>
    /// <seealso cref="CameraStart"/>
    /// <seealso cref="CameraStop"/>
    /// <seealso cref="ServoEnable"/>
    /// <seealso cref="ServoDisable"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalInputs"/>
    /// <seealso cref="Camera0Trigger"/>
    /// <seealso cref="Camera1Trigger"/>
    /// <seealso cref="Camera0Sync"/>
    /// <seealso cref="Camera1Sync"/>
    /// <seealso cref="ServoState"/>
    /// <seealso cref="SyncInterval"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="Control0Mode"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Servo0Period"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Control1Mode"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="Servo1Period"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(CameraStart))]
    [XmlInclude(typeof(CameraStop))]
    [XmlInclude(typeof(ServoEnable))]
    [XmlInclude(typeof(ServoDisable))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalInputs))]
    [XmlInclude(typeof(Camera0Trigger))]
    [XmlInclude(typeof(Camera1Trigger))]
    [XmlInclude(typeof(Camera0Sync))]
    [XmlInclude(typeof(Camera1Sync))]
    [XmlInclude(typeof(ServoState))]
    [XmlInclude(typeof(SyncInterval))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(Control0Mode))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Servo0Period))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Control1Mode))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(Servo1Period))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Filters register-specific messages reported by the CameraController device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new CameraStart();
        }

        string INamedElement.Name
        {
            get => $"{nameof(CameraController)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the CameraController device.
    /// </summary>
    /// <seealso cref="CameraStart"/>
    /// <seealso cref="CameraStop"/>
    /// <seealso cref="ServoEnable"/>
    /// <seealso cref="ServoDisable"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalInputs"/>
    /// <seealso cref="Camera0Trigger"/>
    /// <seealso cref="Camera1Trigger"/>
    /// <seealso cref="Camera0Sync"/>
    /// <seealso cref="Camera1Sync"/>
    /// <seealso cref="ServoState"/>
    /// <seealso cref="SyncInterval"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="Control0Mode"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Servo0Period"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Control1Mode"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="Servo1Period"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(CameraStart))]
    [XmlInclude(typeof(CameraStop))]
    [XmlInclude(typeof(ServoEnable))]
    [XmlInclude(typeof(ServoDisable))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalInputs))]
    [XmlInclude(typeof(Camera0Trigger))]
    [XmlInclude(typeof(Camera1Trigger))]
    [XmlInclude(typeof(Camera0Sync))]
    [XmlInclude(typeof(Camera1Sync))]
    [XmlInclude(typeof(ServoState))]
    [XmlInclude(typeof(SyncInterval))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(Control0Mode))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Servo0Period))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Control1Mode))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(Servo1Period))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(EnableEvents))]
    [XmlInclude(typeof(TimestampedCameraStart))]
    [XmlInclude(typeof(TimestampedCameraStop))]
    [XmlInclude(typeof(TimestampedServoEnable))]
    [XmlInclude(typeof(TimestampedServoDisable))]
    [XmlInclude(typeof(TimestampedOutputSet))]
    [XmlInclude(typeof(TimestampedOutputClear))]
    [XmlInclude(typeof(TimestampedOutputState))]
    [XmlInclude(typeof(TimestampedDigitalInputs))]
    [XmlInclude(typeof(TimestampedCamera0Trigger))]
    [XmlInclude(typeof(TimestampedCamera1Trigger))]
    [XmlInclude(typeof(TimestampedCamera0Sync))]
    [XmlInclude(typeof(TimestampedCamera1Sync))]
    [XmlInclude(typeof(TimestampedServoState))]
    [XmlInclude(typeof(TimestampedSyncInterval))]
    [XmlInclude(typeof(TimestampedDI0Mode))]
    [XmlInclude(typeof(TimestampedControl0Mode))]
    [XmlInclude(typeof(TimestampedCamera0Frequency))]
    [XmlInclude(typeof(TimestampedServo0Period))]
    [XmlInclude(typeof(TimestampedServo0PulseWidth))]
    [XmlInclude(typeof(TimestampedControl1Mode))]
    [XmlInclude(typeof(TimestampedCamera1Frequency))]
    [XmlInclude(typeof(TimestampedServo1Period))]
    [XmlInclude(typeof(TimestampedServo1PulseWidth))]
    [XmlInclude(typeof(TimestampedEnableEvents))]
    [Description("Filters and selects specific messages reported by the CameraController device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new CameraStart();
        }

        string INamedElement.Name => $"{nameof(CameraController)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// CameraController register messages.
    /// </summary>
    /// <seealso cref="CameraStart"/>
    /// <seealso cref="CameraStop"/>
    /// <seealso cref="ServoEnable"/>
    /// <seealso cref="ServoDisable"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalInputs"/>
    /// <seealso cref="Camera0Trigger"/>
    /// <seealso cref="Camera1Trigger"/>
    /// <seealso cref="Camera0Sync"/>
    /// <seealso cref="Camera1Sync"/>
    /// <seealso cref="ServoState"/>
    /// <seealso cref="SyncInterval"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="Control0Mode"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Servo0Period"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Control1Mode"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="Servo1Period"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(CameraStart))]
    [XmlInclude(typeof(CameraStop))]
    [XmlInclude(typeof(ServoEnable))]
    [XmlInclude(typeof(ServoDisable))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalInputs))]
    [XmlInclude(typeof(Camera0Trigger))]
    [XmlInclude(typeof(Camera1Trigger))]
    [XmlInclude(typeof(Camera0Sync))]
    [XmlInclude(typeof(Camera1Sync))]
    [XmlInclude(typeof(ServoState))]
    [XmlInclude(typeof(SyncInterval))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(Control0Mode))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Servo0Period))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Control1Mode))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(Servo1Period))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Formats a sequence of values as specific CameraController register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new CameraStart();
        }

        string INamedElement.Name => $"{nameof(CameraController)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that starts the generation of triggers on the specified camera lines.
    /// </summary>
    [Description("Starts the generation of triggers on the specified camera lines.")]
    public partial class CameraStart
    {
        /// <summary>
        /// Represents the address of the <see cref="CameraStart"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="CameraStart"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="CameraStart"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="CameraStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Cameras GetPayload(HarpMessage message)
        {
            return (Cameras)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="CameraStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Cameras> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Cameras)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="CameraStart"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="CameraStart"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Cameras value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="CameraStart"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="CameraStart"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Cameras value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// CameraStart register.
    /// </summary>
    /// <seealso cref="CameraStart"/>
    [Description("Filters and selects timestamped messages from the CameraStart register.")]
    public partial class TimestampedCameraStart
    {
        /// <summary>
        /// Represents the address of the <see cref="CameraStart"/> register. This field is constant.
        /// </summary>
        public const int Address = CameraStart.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="CameraStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Cameras> GetPayload(HarpMessage message)
        {
            return CameraStart.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the generation of triggers on the specified camera lines.
    /// </summary>
    [Description("Stops the generation of triggers on the specified camera lines.")]
    public partial class CameraStop
    {
        /// <summary>
        /// Represents the address of the <see cref="CameraStop"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="CameraStop"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="CameraStop"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="CameraStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Cameras GetPayload(HarpMessage message)
        {
            return (Cameras)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="CameraStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Cameras> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Cameras)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="CameraStop"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="CameraStop"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Cameras value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="CameraStop"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="CameraStop"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Cameras value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// CameraStop register.
    /// </summary>
    /// <seealso cref="CameraStop"/>
    [Description("Filters and selects timestamped messages from the CameraStop register.")]
    public partial class TimestampedCameraStop
    {
        /// <summary>
        /// Represents the address of the <see cref="CameraStop"/> register. This field is constant.
        /// </summary>
        public const int Address = CameraStop.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="CameraStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Cameras> GetPayload(HarpMessage message)
        {
            return CameraStop.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables servo control on the specified camera lines.
    /// </summary>
    [Description("Enables servo control on the specified camera lines.")]
    public partial class ServoEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ServoEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Servos GetPayload(HarpMessage message)
        {
            return (Servos)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Servos)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoEnable register.
    /// </summary>
    /// <seealso cref="ServoEnable"/>
    [Description("Filters and selects timestamped messages from the ServoEnable register.")]
    public partial class TimestampedServoEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetPayload(HarpMessage message)
        {
            return ServoEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that disables servo control on the specified camera lines.
    /// </summary>
    [Description("Disables servo control on the specified camera lines.")]
    public partial class ServoDisable
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoDisable"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoDisable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ServoDisable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Servos GetPayload(HarpMessage message)
        {
            return (Servos)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Servos)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoDisable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoDisable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoDisable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoDisable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoDisable register.
    /// </summary>
    /// <seealso cref="ServoDisable"/>
    [Description("Filters and selects timestamped messages from the ServoDisable register.")]
    public partial class TimestampedServoDisable
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoDisable"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoDisable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetPayload(HarpMessage message)
        {
            return ServoDisable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputSet register.
    /// </summary>
    /// <seealso cref="OutputSet"/>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines")]
    public partial class OutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputClear register.
    /// </summary>
    /// <seealso cref="OutputClear"/>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines.
    /// </summary>
    [Description("Write the state of all digital output lines")]
    public partial class OutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputState register.
    /// </summary>
    /// <seealso cref="OutputState"/>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emits an event when the state of the digital input line changes.
    /// </summary>
    [Description("Emits an event when the state of the digital input line changes.")]
    public partial class DigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputs"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputs"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputs"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputs"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputs"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputs register.
    /// </summary>
    /// <seealso cref="DigitalInputs"/>
    [Description("Filters and selects timestamped messages from the DigitalInputs register.")]
    public partial class TimestampedDigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputs.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputs.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emits an event when a frame is triggered on camera 0.
    /// </summary>
    [Description("Emits an event when a frame is triggered on camera 0.")]
    public partial class Camera0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera0Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera0Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera0Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera0Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera0Trigger register.
    /// </summary>
    /// <seealso cref="Camera0Trigger"/>
    [Description("Filters and selects timestamped messages from the Camera0Trigger register.")]
    public partial class TimestampedCamera0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera0Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Camera0Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emits an event when a frame is triggered on camera 1.
    /// </summary>
    [Description("Emits an event when a frame is triggered on camera 1.")]
    public partial class Camera1Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera1Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera1Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera1Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera1Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera1Trigger register.
    /// </summary>
    /// <seealso cref="Camera1Trigger"/>
    [Description("Filters and selects timestamped messages from the Camera1Trigger register.")]
    public partial class TimestampedCamera1Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera1Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Camera1Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [Description("Emits an event when a sync state is toggled on camera 0.")]
    public partial class Camera0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera0Sync"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera0Sync"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera0Sync"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Sync"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera0Sync"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Sync"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera0Sync register.
    /// </summary>
    /// <seealso cref="Camera0Sync"/>
    [Description("Filters and selects timestamped messages from the Camera0Sync register.")]
    public partial class TimestampedCamera0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera0Sync.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Camera0Sync.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [Description("Emits an event when a sync state is toggled on camera 0.")]
    public partial class Camera1Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera1Sync"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera1Sync"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera1Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera1Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera1Sync"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Sync"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera1Sync"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Sync"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera1Sync register.
    /// </summary>
    /// <seealso cref="Camera1Sync"/>
    [Description("Filters and selects timestamped messages from the Camera1Sync register.")]
    public partial class TimestampedCamera1Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera1Sync.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera1Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Camera1Sync.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that returns the current state of the servo motors.
    /// </summary>
    [Description("Returns the current state of the servo motors.")]
    public partial class ServoState
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoState"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ServoState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Servos GetPayload(HarpMessage message)
        {
            return (Servos)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Servos)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Servos value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoState register.
    /// </summary>
    /// <seealso cref="ServoState"/>
    [Description("Filters and selects timestamped messages from the ServoState register.")]
    public partial class TimestampedServoState
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoState"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Servos> GetPayload(HarpMessage message)
        {
            return ServoState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the interval in seconds between each sync pulse.
    /// </summary>
    [Description("Configures the interval in seconds between each sync pulse")]
    public partial class SyncInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="SyncInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SyncInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="SyncInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SyncInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SyncInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SyncInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SyncInterval register.
    /// </summary>
    /// <seealso cref="SyncInterval"/>
    [Description("Filters and selects timestamped messages from the SyncInterval register.")]
    public partial class TimestampedSyncInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = SyncInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SyncInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return SyncInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the mode of the digital input line 0.
    /// </summary>
    [Description("Configures the mode of the digital input line 0.")]
    public partial class DI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DI0ModeConfig GetPayload(HarpMessage message)
        {
            return (DI0ModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0ModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DI0ModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DI0ModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DI0ModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0Mode register.
    /// </summary>
    /// <seealso cref="DI0Mode"/>
    [Description("Filters and selects timestamped messages from the DI0Mode register.")]
    public partial class TimestampedDI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0ModeConfig> GetPayload(HarpMessage message)
        {
            return DI0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the control mode of Camera/Servo 0.
    /// </summary>
    [Description("Configures the control mode of Camera/Servo 0.")]
    public partial class Control0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="Control0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Control0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Control0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Control0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ControlModeConfig GetPayload(HarpMessage message)
        {
            return (ControlModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Control0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ControlModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ControlModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Control0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Control0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ControlModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Control0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Control0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ControlModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Control0Mode register.
    /// </summary>
    /// <seealso cref="Control0Mode"/>
    [Description("Filters and selects timestamped messages from the Control0Mode register.")]
    public partial class TimestampedControl0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="Control0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = Control0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Control0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ControlModeConfig> GetPayload(HarpMessage message)
        {
            return Control0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
    /// </summary>
    [Description("Configures the frequency of the trigger pulses on Camera 0 when using Camera mode.")]
    public partial class Camera0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera0Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera0Frequency register.
    /// </summary>
    /// <seealso cref="Camera0Frequency"/>
    [Description("Filters and selects timestamped messages from the Camera0Frequency register.")]
    public partial class TimestampedCamera0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera0Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Camera0Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [Description("Configures the servo motor period (us) when using Servo mode (sensitive to 2 us)")]
    public partial class Servo0Period
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0Period"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo0Period"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo0Period"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo0Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo0Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo0Period"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0Period"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo0Period"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0Period"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo0Period register.
    /// </summary>
    /// <seealso cref="Servo0Period"/>
    [Description("Filters and selects timestamped messages from the Servo0Period register.")]
    public partial class TimestampedServo0Period
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0Period"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo0Period.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo0Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo0Period.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [Description("Configures the servo pulse width (us) when using Servo mode (sensitive to 2 us)")]
    public partial class Servo0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo0PulseWidth register.
    /// </summary>
    /// <seealso cref="Servo0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Servo0PulseWidth register.")]
    public partial class TimestampedServo0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the control mode of Camera/Servo 1.
    /// </summary>
    [Description("Configures the control mode of Camera/Servo 1.")]
    public partial class Control1Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="Control1Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="Control1Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Control1Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Control1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ControlModeConfig GetPayload(HarpMessage message)
        {
            return (ControlModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Control1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ControlModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ControlModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Control1Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Control1Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ControlModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Control1Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Control1Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ControlModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Control1Mode register.
    /// </summary>
    /// <seealso cref="Control1Mode"/>
    [Description("Filters and selects timestamped messages from the Control1Mode register.")]
    public partial class TimestampedControl1Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="Control1Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = Control1Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Control1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ControlModeConfig> GetPayload(HarpMessage message)
        {
            return Control1Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
    /// </summary>
    [Description("Configures the frequency of the trigger pulses on Camera 1 when using Camera mode.")]
    public partial class Camera1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera1Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera1Frequency register.
    /// </summary>
    /// <seealso cref="Camera1Frequency"/>
    [Description("Filters and selects timestamped messages from the Camera1Frequency register.")]
    public partial class TimestampedCamera1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera1Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Camera1Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [Description("Configures the servo motor period (us) when using Servo mode (sensitive to 2 us)")]
    public partial class Servo1Period
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1Period"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo1Period"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo1Period"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo1Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo1Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo1Period"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1Period"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo1Period"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1Period"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo1Period register.
    /// </summary>
    /// <seealso cref="Servo1Period"/>
    [Description("Filters and selects timestamped messages from the Servo1Period register.")]
    public partial class TimestampedServo1Period
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1Period"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo1Period.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo1Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo1Period.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [Description("Configures the servo pulse width (us) when using Servo mode (sensitive to 2 us)")]
    public partial class Servo1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo1PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo1PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo1PulseWidth register.
    /// </summary>
    /// <seealso cref="Servo1PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Servo1PulseWidth register.")]
    public partial class TimestampedServo1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo1PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo1PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the active events in the device.
    /// </summary>
    [Description("Specifies the active events in the device.")]
    public partial class EnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static CameraControllerEvents GetPayload(HarpMessage message)
        {
            return (CameraControllerEvents)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraControllerEvents> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((CameraControllerEvents)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableEvents"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, CameraControllerEvents value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableEvents"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, CameraControllerEvents value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableEvents register.
    /// </summary>
    /// <seealso cref="EnableEvents"/>
    [Description("Filters and selects timestamped messages from the EnableEvents register.")]
    public partial class TimestampedEnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableEvents.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraControllerEvents> GetPayload(HarpMessage message)
        {
            return EnableEvents.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// CameraController device.
    /// </summary>
    /// <seealso cref="CreateCameraStartPayload"/>
    /// <seealso cref="CreateCameraStopPayload"/>
    /// <seealso cref="CreateServoEnablePayload"/>
    /// <seealso cref="CreateServoDisablePayload"/>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreateDigitalInputsPayload"/>
    /// <seealso cref="CreateCamera0TriggerPayload"/>
    /// <seealso cref="CreateCamera1TriggerPayload"/>
    /// <seealso cref="CreateCamera0SyncPayload"/>
    /// <seealso cref="CreateCamera1SyncPayload"/>
    /// <seealso cref="CreateServoStatePayload"/>
    /// <seealso cref="CreateSyncIntervalPayload"/>
    /// <seealso cref="CreateDI0ModePayload"/>
    /// <seealso cref="CreateControl0ModePayload"/>
    /// <seealso cref="CreateCamera0FrequencyPayload"/>
    /// <seealso cref="CreateServo0PeriodPayload"/>
    /// <seealso cref="CreateServo0PulseWidthPayload"/>
    /// <seealso cref="CreateControl1ModePayload"/>
    /// <seealso cref="CreateCamera1FrequencyPayload"/>
    /// <seealso cref="CreateServo1PeriodPayload"/>
    /// <seealso cref="CreateServo1PulseWidthPayload"/>
    /// <seealso cref="CreateEnableEventsPayload"/>
    [XmlInclude(typeof(CreateCameraStartPayload))]
    [XmlInclude(typeof(CreateCameraStopPayload))]
    [XmlInclude(typeof(CreateServoEnablePayload))]
    [XmlInclude(typeof(CreateServoDisablePayload))]
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreateDigitalInputsPayload))]
    [XmlInclude(typeof(CreateCamera0TriggerPayload))]
    [XmlInclude(typeof(CreateCamera1TriggerPayload))]
    [XmlInclude(typeof(CreateCamera0SyncPayload))]
    [XmlInclude(typeof(CreateCamera1SyncPayload))]
    [XmlInclude(typeof(CreateServoStatePayload))]
    [XmlInclude(typeof(CreateSyncIntervalPayload))]
    [XmlInclude(typeof(CreateDI0ModePayload))]
    [XmlInclude(typeof(CreateControl0ModePayload))]
    [XmlInclude(typeof(CreateCamera0FrequencyPayload))]
    [XmlInclude(typeof(CreateServo0PeriodPayload))]
    [XmlInclude(typeof(CreateServo0PulseWidthPayload))]
    [XmlInclude(typeof(CreateControl1ModePayload))]
    [XmlInclude(typeof(CreateCamera1FrequencyPayload))]
    [XmlInclude(typeof(CreateServo1PeriodPayload))]
    [XmlInclude(typeof(CreateServo1PulseWidthPayload))]
    [XmlInclude(typeof(CreateEnableEventsPayload))]
    [XmlInclude(typeof(CreateTimestampedCameraStartPayload))]
    [XmlInclude(typeof(CreateTimestampedCameraStopPayload))]
    [XmlInclude(typeof(CreateTimestampedServoEnablePayload))]
    [XmlInclude(typeof(CreateTimestampedServoDisablePayload))]
    [XmlInclude(typeof(CreateTimestampedOutputSetPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputClearPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputsPayload))]
    [XmlInclude(typeof(CreateTimestampedCamera0TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedCamera1TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedCamera0SyncPayload))]
    [XmlInclude(typeof(CreateTimestampedCamera1SyncPayload))]
    [XmlInclude(typeof(CreateTimestampedServoStatePayload))]
    [XmlInclude(typeof(CreateTimestampedSyncIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedDI0ModePayload))]
    [XmlInclude(typeof(CreateTimestampedControl0ModePayload))]
    [XmlInclude(typeof(CreateTimestampedCamera0FrequencyPayload))]
    [XmlInclude(typeof(CreateTimestampedServo0PeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedServo0PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedControl1ModePayload))]
    [XmlInclude(typeof(CreateTimestampedCamera1FrequencyPayload))]
    [XmlInclude(typeof(CreateTimestampedServo1PeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedServo1PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableEventsPayload))]
    [Description("Creates standard message payloads for the CameraController device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateCameraStartPayload();
        }

        string INamedElement.Name => $"{nameof(CameraController)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the generation of triggers on the specified camera lines.
    /// </summary>
    [DisplayName("CameraStartPayload")]
    [Description("Creates a message payload that starts the generation of triggers on the specified camera lines.")]
    public partial class CreateCameraStartPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the generation of triggers on the specified camera lines.
        /// </summary>
        [Description("The value that starts the generation of triggers on the specified camera lines.")]
        public Cameras CameraStart { get; set; }

        /// <summary>
        /// Creates a message payload for the CameraStart register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Cameras GetPayload()
        {
            return CameraStart;
        }

        /// <summary>
        /// Creates a message that starts the generation of triggers on the specified camera lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the CameraStart register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.CameraStart.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the generation of triggers on the specified camera lines.
    /// </summary>
    [DisplayName("TimestampedCameraStartPayload")]
    [Description("Creates a timestamped message payload that starts the generation of triggers on the specified camera lines.")]
    public partial class CreateTimestampedCameraStartPayload : CreateCameraStartPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the generation of triggers on the specified camera lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the CameraStart register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.CameraStart.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that stops the generation of triggers on the specified camera lines.
    /// </summary>
    [DisplayName("CameraStopPayload")]
    [Description("Creates a message payload that stops the generation of triggers on the specified camera lines.")]
    public partial class CreateCameraStopPayload
    {
        /// <summary>
        /// Gets or sets the value that stops the generation of triggers on the specified camera lines.
        /// </summary>
        [Description("The value that stops the generation of triggers on the specified camera lines.")]
        public Cameras CameraStop { get; set; }

        /// <summary>
        /// Creates a message payload for the CameraStop register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Cameras GetPayload()
        {
            return CameraStop;
        }

        /// <summary>
        /// Creates a message that stops the generation of triggers on the specified camera lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the CameraStop register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.CameraStop.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that stops the generation of triggers on the specified camera lines.
    /// </summary>
    [DisplayName("TimestampedCameraStopPayload")]
    [Description("Creates a timestamped message payload that stops the generation of triggers on the specified camera lines.")]
    public partial class CreateTimestampedCameraStopPayload : CreateCameraStopPayload
    {
        /// <summary>
        /// Creates a timestamped message that stops the generation of triggers on the specified camera lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the CameraStop register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.CameraStop.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables servo control on the specified camera lines.
    /// </summary>
    [DisplayName("ServoEnablePayload")]
    [Description("Creates a message payload that enables servo control on the specified camera lines.")]
    public partial class CreateServoEnablePayload
    {
        /// <summary>
        /// Gets or sets the value that enables servo control on the specified camera lines.
        /// </summary>
        [Description("The value that enables servo control on the specified camera lines.")]
        public Servos ServoEnable { get; set; }

        /// <summary>
        /// Creates a message payload for the ServoEnable register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Servos GetPayload()
        {
            return ServoEnable;
        }

        /// <summary>
        /// Creates a message that enables servo control on the specified camera lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoEnable register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.ServoEnable.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables servo control on the specified camera lines.
    /// </summary>
    [DisplayName("TimestampedServoEnablePayload")]
    [Description("Creates a timestamped message payload that enables servo control on the specified camera lines.")]
    public partial class CreateTimestampedServoEnablePayload : CreateServoEnablePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables servo control on the specified camera lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoEnable register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.ServoEnable.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that disables servo control on the specified camera lines.
    /// </summary>
    [DisplayName("ServoDisablePayload")]
    [Description("Creates a message payload that disables servo control on the specified camera lines.")]
    public partial class CreateServoDisablePayload
    {
        /// <summary>
        /// Gets or sets the value that disables servo control on the specified camera lines.
        /// </summary>
        [Description("The value that disables servo control on the specified camera lines.")]
        public Servos ServoDisable { get; set; }

        /// <summary>
        /// Creates a message payload for the ServoDisable register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Servos GetPayload()
        {
            return ServoDisable;
        }

        /// <summary>
        /// Creates a message that disables servo control on the specified camera lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoDisable register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.ServoDisable.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that disables servo control on the specified camera lines.
    /// </summary>
    [DisplayName("TimestampedServoDisablePayload")]
    [Description("Creates a timestamped message payload that disables servo control on the specified camera lines.")]
    public partial class CreateTimestampedServoDisablePayload : CreateServoDisablePayload
    {
        /// <summary>
        /// Creates a timestamped message that disables servo control on the specified camera lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoDisable register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.ServoDisable.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [Description("Creates a message payload that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs OutputSet { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputSet;
        }

        /// <summary>
        /// Creates a message that set the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.OutputSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputSetPayload")]
    [Description("Creates a timestamped message payload that set the specified digital output lines.")]
    public partial class CreateTimestampedOutputSetPayload : CreateOutputSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.OutputSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [Description("Creates a message payload that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs OutputClear { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputClear;
        }

        /// <summary>
        /// Creates a message that clear the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.OutputClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified digital output lines.")]
    public partial class CreateTimestampedOutputClearPayload : CreateOutputClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.OutputClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [Description("Creates a message payload that write the state of all digital output lines.")]
    public partial class CreateOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines.
        /// </summary>
        [Description("The value that write the state of all digital output lines.")]
        public DigitalOutputs OutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputState;
        }

        /// <summary>
        /// Creates a message that write the state of all digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.OutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all digital output lines.")]
    public partial class CreateTimestampedOutputStatePayload : CreateOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.OutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emits an event when the state of the digital input line changes.
    /// </summary>
    [DisplayName("DigitalInputsPayload")]
    [Description("Creates a message payload that emits an event when the state of the digital input line changes.")]
    public partial class CreateDigitalInputsPayload
    {
        /// <summary>
        /// Gets or sets the value that emits an event when the state of the digital input line changes.
        /// </summary>
        [Description("The value that emits an event when the state of the digital input line changes.")]
        public DigitalInputs DigitalInputs { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputs register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputs;
        }

        /// <summary>
        /// Creates a message that emits an event when the state of the digital input line changes.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputs register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.DigitalInputs.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emits an event when the state of the digital input line changes.
    /// </summary>
    [DisplayName("TimestampedDigitalInputsPayload")]
    [Description("Creates a timestamped message payload that emits an event when the state of the digital input line changes.")]
    public partial class CreateTimestampedDigitalInputsPayload : CreateDigitalInputsPayload
    {
        /// <summary>
        /// Creates a timestamped message that emits an event when the state of the digital input line changes.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputs register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.DigitalInputs.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emits an event when a frame is triggered on camera 0.
    /// </summary>
    [DisplayName("Camera0TriggerPayload")]
    [Description("Creates a message payload that emits an event when a frame is triggered on camera 0.")]
    public partial class CreateCamera0TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that emits an event when a frame is triggered on camera 0.
        /// </summary>
        [Description("The value that emits an event when a frame is triggered on camera 0.")]
        public byte Camera0Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera0Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Camera0Trigger;
        }

        /// <summary>
        /// Creates a message that emits an event when a frame is triggered on camera 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera0Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera0Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emits an event when a frame is triggered on camera 0.
    /// </summary>
    [DisplayName("TimestampedCamera0TriggerPayload")]
    [Description("Creates a timestamped message payload that emits an event when a frame is triggered on camera 0.")]
    public partial class CreateTimestampedCamera0TriggerPayload : CreateCamera0TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that emits an event when a frame is triggered on camera 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera0Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera0Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emits an event when a frame is triggered on camera 1.
    /// </summary>
    [DisplayName("Camera1TriggerPayload")]
    [Description("Creates a message payload that emits an event when a frame is triggered on camera 1.")]
    public partial class CreateCamera1TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that emits an event when a frame is triggered on camera 1.
        /// </summary>
        [Description("The value that emits an event when a frame is triggered on camera 1.")]
        public byte Camera1Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera1Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Camera1Trigger;
        }

        /// <summary>
        /// Creates a message that emits an event when a frame is triggered on camera 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera1Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera1Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emits an event when a frame is triggered on camera 1.
    /// </summary>
    [DisplayName("TimestampedCamera1TriggerPayload")]
    [Description("Creates a timestamped message payload that emits an event when a frame is triggered on camera 1.")]
    public partial class CreateTimestampedCamera1TriggerPayload : CreateCamera1TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that emits an event when a frame is triggered on camera 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera1Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera1Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [DisplayName("Camera0SyncPayload")]
    [Description("Creates a message payload that emits an event when a sync state is toggled on camera 0.")]
    public partial class CreateCamera0SyncPayload
    {
        /// <summary>
        /// Gets or sets the value that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        [Description("The value that emits an event when a sync state is toggled on camera 0.")]
        public byte Camera0Sync { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera0Sync register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Camera0Sync;
        }

        /// <summary>
        /// Creates a message that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera0Sync register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera0Sync.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [DisplayName("TimestampedCamera0SyncPayload")]
    [Description("Creates a timestamped message payload that emits an event when a sync state is toggled on camera 0.")]
    public partial class CreateTimestampedCamera0SyncPayload : CreateCamera0SyncPayload
    {
        /// <summary>
        /// Creates a timestamped message that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera0Sync register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera0Sync.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [DisplayName("Camera1SyncPayload")]
    [Description("Creates a message payload that emits an event when a sync state is toggled on camera 0.")]
    public partial class CreateCamera1SyncPayload
    {
        /// <summary>
        /// Gets or sets the value that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        [Description("The value that emits an event when a sync state is toggled on camera 0.")]
        public byte Camera1Sync { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera1Sync register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Camera1Sync;
        }

        /// <summary>
        /// Creates a message that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera1Sync register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera1Sync.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emits an event when a sync state is toggled on camera 0.
    /// </summary>
    [DisplayName("TimestampedCamera1SyncPayload")]
    [Description("Creates a timestamped message payload that emits an event when a sync state is toggled on camera 0.")]
    public partial class CreateTimestampedCamera1SyncPayload : CreateCamera1SyncPayload
    {
        /// <summary>
        /// Creates a timestamped message that emits an event when a sync state is toggled on camera 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera1Sync register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera1Sync.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that returns the current state of the servo motors.
    /// </summary>
    [DisplayName("ServoStatePayload")]
    [Description("Creates a message payload that returns the current state of the servo motors.")]
    public partial class CreateServoStatePayload
    {
        /// <summary>
        /// Gets or sets the value that returns the current state of the servo motors.
        /// </summary>
        [Description("The value that returns the current state of the servo motors.")]
        public Servos ServoState { get; set; }

        /// <summary>
        /// Creates a message payload for the ServoState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Servos GetPayload()
        {
            return ServoState;
        }

        /// <summary>
        /// Creates a message that returns the current state of the servo motors.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.ServoState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that returns the current state of the servo motors.
    /// </summary>
    [DisplayName("TimestampedServoStatePayload")]
    [Description("Creates a timestamped message payload that returns the current state of the servo motors.")]
    public partial class CreateTimestampedServoStatePayload : CreateServoStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that returns the current state of the servo motors.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.ServoState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the interval in seconds between each sync pulse.
    /// </summary>
    [DisplayName("SyncIntervalPayload")]
    [Description("Creates a message payload that configures the interval in seconds between each sync pulse.")]
    public partial class CreateSyncIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the interval in seconds between each sync pulse.
        /// </summary>
        [Description("The value that configures the interval in seconds between each sync pulse.")]
        public byte SyncInterval { get; set; }

        /// <summary>
        /// Creates a message payload for the SyncInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return SyncInterval;
        }

        /// <summary>
        /// Creates a message that configures the interval in seconds between each sync pulse.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SyncInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.SyncInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the interval in seconds between each sync pulse.
    /// </summary>
    [DisplayName("TimestampedSyncIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the interval in seconds between each sync pulse.")]
    public partial class CreateTimestampedSyncIntervalPayload : CreateSyncIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the interval in seconds between each sync pulse.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SyncInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.SyncInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the mode of the digital input line 0.
    /// </summary>
    [DisplayName("DI0ModePayload")]
    [Description("Creates a message payload that configures the mode of the digital input line 0.")]
    public partial class CreateDI0ModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the mode of the digital input line 0.
        /// </summary>
        [Description("The value that configures the mode of the digital input line 0.")]
        public DI0ModeConfig DI0Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the DI0Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DI0ModeConfig GetPayload()
        {
            return DI0Mode;
        }

        /// <summary>
        /// Creates a message that configures the mode of the digital input line 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DI0Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.DI0Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the mode of the digital input line 0.
    /// </summary>
    [DisplayName("TimestampedDI0ModePayload")]
    [Description("Creates a timestamped message payload that configures the mode of the digital input line 0.")]
    public partial class CreateTimestampedDI0ModePayload : CreateDI0ModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the mode of the digital input line 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DI0Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.DI0Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the control mode of Camera/Servo 0.
    /// </summary>
    [DisplayName("Control0ModePayload")]
    [Description("Creates a message payload that configures the control mode of Camera/Servo 0.")]
    public partial class CreateControl0ModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the control mode of Camera/Servo 0.
        /// </summary>
        [Description("The value that configures the control mode of Camera/Servo 0.")]
        public ControlModeConfig Control0Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the Control0Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ControlModeConfig GetPayload()
        {
            return Control0Mode;
        }

        /// <summary>
        /// Creates a message that configures the control mode of Camera/Servo 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Control0Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Control0Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the control mode of Camera/Servo 0.
    /// </summary>
    [DisplayName("TimestampedControl0ModePayload")]
    [Description("Creates a timestamped message payload that configures the control mode of Camera/Servo 0.")]
    public partial class CreateTimestampedControl0ModePayload : CreateControl0ModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the control mode of Camera/Servo 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Control0Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Control0Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
    /// </summary>
    [DisplayName("Camera0FrequencyPayload")]
    [Description("Creates a message payload that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.")]
    public partial class CreateCamera0FrequencyPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.")]
        public ushort Camera0Frequency { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the Camera0Frequency register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Camera0Frequency;
        }

        /// <summary>
        /// Creates a message that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera0Frequency register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera0Frequency.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
    /// </summary>
    [DisplayName("TimestampedCamera0FrequencyPayload")]
    [Description("Creates a timestamped message payload that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.")]
    public partial class CreateTimestampedCamera0FrequencyPayload : CreateCamera0FrequencyPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the frequency of the trigger pulses on Camera 0 when using Camera mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera0Frequency register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera0Frequency.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("Servo0PeriodPayload")]
    [Description("Creates a message payload that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateServo0PeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        [Description("The value that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
        public ushort Servo0Period { get; set; }

        /// <summary>
        /// Creates a message payload for the Servo0Period register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Servo0Period;
        }

        /// <summary>
        /// Creates a message that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Servo0Period register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Servo0Period.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("TimestampedServo0PeriodPayload")]
    [Description("Creates a timestamped message payload that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateTimestampedServo0PeriodPayload : CreateServo0PeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Servo0Period register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Servo0Period.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("Servo0PulseWidthPayload")]
    [Description("Creates a message payload that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateServo0PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        [Description("The value that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
        public ushort Servo0PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the Servo0PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Servo0PulseWidth;
        }

        /// <summary>
        /// Creates a message that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Servo0PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Servo0PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("TimestampedServo0PulseWidthPayload")]
    [Description("Creates a timestamped message payload that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateTimestampedServo0PulseWidthPayload : CreateServo0PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Servo0PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Servo0PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the control mode of Camera/Servo 1.
    /// </summary>
    [DisplayName("Control1ModePayload")]
    [Description("Creates a message payload that configures the control mode of Camera/Servo 1.")]
    public partial class CreateControl1ModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the control mode of Camera/Servo 1.
        /// </summary>
        [Description("The value that configures the control mode of Camera/Servo 1.")]
        public ControlModeConfig Control1Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the Control1Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ControlModeConfig GetPayload()
        {
            return Control1Mode;
        }

        /// <summary>
        /// Creates a message that configures the control mode of Camera/Servo 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Control1Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Control1Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the control mode of Camera/Servo 1.
    /// </summary>
    [DisplayName("TimestampedControl1ModePayload")]
    [Description("Creates a timestamped message payload that configures the control mode of Camera/Servo 1.")]
    public partial class CreateTimestampedControl1ModePayload : CreateControl1ModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the control mode of Camera/Servo 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Control1Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Control1Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
    /// </summary>
    [DisplayName("Camera1FrequencyPayload")]
    [Description("Creates a message payload that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.")]
    public partial class CreateCamera1FrequencyPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.")]
        public ushort Camera1Frequency { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the Camera1Frequency register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Camera1Frequency;
        }

        /// <summary>
        /// Creates a message that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera1Frequency register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Camera1Frequency.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
    /// </summary>
    [DisplayName("TimestampedCamera1FrequencyPayload")]
    [Description("Creates a timestamped message payload that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.")]
    public partial class CreateTimestampedCamera1FrequencyPayload : CreateCamera1FrequencyPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the frequency of the trigger pulses on Camera 1 when using Camera mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera1Frequency register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Camera1Frequency.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("Servo1PeriodPayload")]
    [Description("Creates a message payload that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateServo1PeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        [Description("The value that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
        public ushort Servo1Period { get; set; }

        /// <summary>
        /// Creates a message payload for the Servo1Period register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Servo1Period;
        }

        /// <summary>
        /// Creates a message that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Servo1Period register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Servo1Period.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("TimestampedServo1PeriodPayload")]
    [Description("Creates a timestamped message payload that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateTimestampedServo1PeriodPayload : CreateServo1PeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the servo motor period (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Servo1Period register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Servo1Period.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("Servo1PulseWidthPayload")]
    [Description("Creates a message payload that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateServo1PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        [Description("The value that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
        public ushort Servo1PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the Servo1PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Servo1PulseWidth;
        }

        /// <summary>
        /// Creates a message that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Servo1PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.Servo1PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
    /// </summary>
    [DisplayName("TimestampedServo1PulseWidthPayload")]
    [Description("Creates a timestamped message payload that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).")]
    public partial class CreateTimestampedServo1PulseWidthPayload : CreateServo1PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the servo pulse width (us) when using Servo mode (sensitive to 2 us).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Servo1PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.Servo1PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EnableEventsPayload")]
    [Description("Creates a message payload that specifies the active events in the device.")]
    public partial class CreateEnableEventsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public CameraControllerEvents EnableEvents { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableEvents register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public CameraControllerEvents GetPayload()
        {
            return EnableEvents;
        }

        /// <summary>
        /// Creates a message that specifies the active events in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableEvents register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.CameraController.EnableEvents.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("TimestampedEnableEventsPayload")]
    [Description("Creates a timestamped message payload that specifies the active events in the device.")]
    public partial class CreateTimestampedEnableEventsPayload : CreateEnableEventsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the active events in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableEvents register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.CameraController.EnableEvents.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Specifies the target camera line.
    /// </summary>
    [Flags]
    public enum Cameras : byte
    {
        None = 0x0,
        Camera0 = 0x1,
        Camera1 = 0x2
    }

    /// <summary>
    /// Specifies the target servo-motor lines.
    /// </summary>
    [Flags]
    public enum Servos : byte
    {
        None = 0x0,
        Servo0 = 0x1,
        Servo1 = 0x2
    }

    /// <summary>
    /// Available digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        None = 0x0,
        Trigger0 = 0x1,
        Sync0 = 0x2,
        Trigger1 = 0x4,
        Sync1 = 0x8
    }

    /// <summary>
    /// Available digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        DI0 = 0x1
    }

    /// <summary>
    /// Specifies the active events in the device.
    /// </summary>
    [Flags]
    public enum CameraControllerEvents : byte
    {
        /// <summary>
        /// Specifies that no flags are defined.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Enables CameraTrigger and CameraSync events.
        /// </summary>
        TriggerAndSynch = 0x1,

        /// <summary>
        /// Enables DigitalInputs
        /// </summary>
        DigitalInputs = 0x2
    }

    /// <summary>
    /// Specifies the operation mode of digital input line 0.
    /// </summary>
    public enum DI0ModeConfig : byte
    {
        /// <summary>
        /// When High, enables Camera0 or Servo0.
        /// </summary>
        HighEnablesCamera0 = 0,

        /// <summary>
        /// When High, enables Camera1 or Servo1.
        /// </summary>
        HighEnablesCamera1 = 1,

        /// <summary>
        /// When High, enables both Cameras or Servos.
        /// </summary>
        HighEnablesCameraBoth = 2,

        /// <summary>
        /// When Low, enables Camera0 or Servo0.
        /// </summary>
        LowEnablesCamera0 = 3,

        /// <summary>
        /// When Low, enables Camera1 or Servo1.
        /// </summary>
        LowEnablesCamera1 = 4,

        /// <summary>
        /// When Low, enables both Cameras or Servos.
        /// </summary>
        LowEnablesCameraBoth = 5,

        /// <summary>
        /// The line will function as a passive digital input.
        /// </summary>
        Default = 6
    }

    /// <summary>
    /// Specifies the operation mode of a specific output line.
    /// </summary>
    public enum ControlModeConfig : byte
    {
        /// <summary>
        /// Enables Camera mode and it will produce the configured trigger.
        /// </summary>
        Camera = 0,

        /// <summary>
        /// Enables Camera mode and it will produce the configured trigger.
        /// </summary>
        Servo = 1
    }
}
