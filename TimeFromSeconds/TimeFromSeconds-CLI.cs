using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace TimeFromSeconds
{
    class TimeFromSeconds
    {
        //Enumerations

        //Defines Error Codes
        public enum ErrorCode
        {
            Success = 0x0,
            FileNotFound = 0x2,
            InvalidData = 0xD,
            BadArguments = 0xA0,
            ArithmeticOverflow = 0x216,
            InvalidIndex = 0x585
        }//enum ErrorCode

        //Defines Units used
        public enum Unit
        {
            Seconds,
            Minutes,
            Hours,
            Days,
            Weeks,
            Years,
            Decades,
            Centuries,
            Millennia
        }//enum Unit



        //Assignment of CONSTANTs and readonly variables
        private const string DEV = "Chris Judkins";
        private static readonly decimal MILLENNIUM = 31557600000;
        private static readonly Decimal CENTURY = 3155760000;
        private static readonly Decimal DECADE = 315576000;
        private static readonly Decimal YEAR = 31557600;
        private static readonly Decimal WEEK = 604800;
        private static readonly Decimal DAY = 86400;
        private static readonly Decimal HOUR = 3600;
        private static readonly Decimal MINUTE = 60;
        private static readonly decimal DECISECOND = .1M;
        private static readonly decimal CENTISECOND = .01M;
        private static readonly decimal MILLISECOND = .001M;
        private static readonly decimal MYRIOSECOND = 10e-4M;
        private static readonly decimal MICROSECOND = 10e-6M;
        private static readonly decimal NANOSECOND = 10e-9M;
        private static readonly decimal PICOSECOND = 10e-12M;
        private static readonly decimal FEMTOSECOND = 10e-15M;



        //Asignment of variables
        private static bool debugMode;
        private decimal input;
        private bool subSecond;
        private static List<ErrorCode> eList = new List<ErrorCode>();



        //Properties to access variables
        private decimal Input
        {
            get { return this.input; }
            set
            {
                if (value >= 0)
                    this.input = value;
            }//set
        }//Input

        private bool SubSecondOutput
        {
            get { return this.subSecond; }
            set
            {
                this.subSecond = value;
            }
        }

        public static bool DebugMode
        {
            get
            {
                return debugMode;
            }//get
            set
            {
                debugMode = value;
            }//set
        }//DebugMode



        //Auto-implemented Properties
        private Unit TFSUnit { get; set; }



        //Constructors
        public TimeFromSeconds(Unit aUnit, decimal aInput, bool aSubSecondOutput = false)
        {
            TFSUnit = aUnit;
            Input = aInput;
            SubSecondOutput = aSubSecondOutput;
        }//TimeFromSeconds()



        //Methods

        //ToString() allows an object of the class to be called where a string input is expected (i.e. Console.WriteLine(aTFS);)
        public override string ToString()
        {
            try
            {
                return FromSeconds(); //FromSeconds method returns a string version of the input time
            }//try
            catch (OverflowException e)
            {
                eList.Add(ErrorCode.ArithmeticOverflow);
                Error(e);
                return e.Message;
            }//catch overflow
            catch (Exception e)
            {
                Error(e);
                return e.Message;
            }//catch
        }//ToString()

        //Primary method for performing conversion
        private string FromSeconds()
        {
            //Dec and Init local vars
            ulong millennia = 0;
            ulong centuries = 0;
            ulong decades = 0;
            ulong years = 0;
            ulong weeks = 0;
            ulong days = 0;
            ulong hours = 0;
            ulong minutes = 0;
            ulong seconds = 0;
            ulong deciseconds = 0;
            ulong centiseconds = 0;
            ulong milliseconds = 0;
            ulong myrioseconds = 0;
            ulong microseconds = 0;
            ulong nanoseconds = 0;
            ulong picoseconds = 0;
            ulong femtoseconds = 0;
            ulong temp = 0;
            decimal inVar = this.Input;
            decimal original = inVar;
            decimal originalSeconds = 0;
            bool subSecondOutput = this.SubSecondOutput;
            StringBuilder sb = new StringBuilder();
            //Covert from input unit type to seconds
            if (TFSUnit == Unit.Minutes)
            {
                inVar *= MINUTE;
            }//if minutes

            else if (TFSUnit == Unit.Hours)
            {
                inVar *= HOUR;
            }//if hours

            else if (TFSUnit == Unit.Days)
            {
                inVar *= DAY;
            }//if days

            else if (TFSUnit == Unit.Weeks)
            {
                inVar *= WEEK;
            }//if hours

            else if (TFSUnit == Unit.Years)
            {
                inVar *= YEAR;
            }//if years

            else if (TFSUnit == Unit.Decades)
            {
                inVar *= DECADE;
            }//if decades

            else if (TFSUnit == Unit.Centuries)
            {
                inVar *= CENTURY;
            }//if centuries

            else if (TFSUnit == Unit.Millennia)
            {
                inVar *= MILLENNIUM;
            }//if Millennia

            //Check if input is negative
            if (this.Input < 0)
            {
                eList.Add(ErrorCode.BadArguments);
                throw new ArgumentOutOfRangeException("Time cannot be negative. Input was: " + Input);
            }
            else
            {
                originalSeconds = inVar;
                sb.Append($"{original} {TFSUnit} is:");
                while (inVar >= FEMTOSECOND)
                {
                    MyDebugger($"Debug: original = {original}\nTFSUnit = {TFSUnit}\noriginalSeconds = {originalSeconds}\ninVar = {inVar}\n" +
                    $"millennia = {millennia}\ncenturies = {centuries}\ndecades = {decades}\nyears = {years}\nweeks = {weeks}\ndays = {days}\n" +
                    $"hours = {hours}\nminutes = {minutes}\nseconds = {seconds}\n" +
                    $"deciseconds = {deciseconds}\ncentiseconds = {centiseconds}\nmilliseconds = {milliseconds}\n" +
                    $"myrioseconds = {myrioseconds}\nmicroseconds = {microseconds}\nnanoseconds = {nanoseconds}\npicoseconds = {picoseconds}\nfemtoseconds = {femtoseconds}");

                    // Break down seconds starting with largest unit and moving down to smallest unit until inVar < FEMTOSECOND
                    if (inVar >= MILLENNIUM && ((TFSUnit != Unit.Millennia) || (originalSeconds % MILLENNIUM != 0))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / MILLENNIUM);
                        millennia += temp;
                        inVar -= (MILLENNIUM * temp);
                        sb.Append($" {millennia} millennia,");
                    }//Millennia

                    else if (inVar >= CENTURY && ((TFSUnit != Unit.Centuries) || (originalSeconds % CENTURY != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / CENTURY);
                        centuries += temp;
                        inVar -= (CENTURY * temp);
                        sb.Append($" {centuries} centuries,");
                    }//centuries

                    else if (inVar >= DECADE && ((TFSUnit != Unit.Decades) || (originalSeconds % DECADE != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / DECADE);
                        decades += temp;
                        inVar -= (DECADE * temp);
                        sb.Append($" {decades} decades,");
                    }//decades

                    else if (inVar >= YEAR && ((TFSUnit != Unit.Years) || (originalSeconds % YEAR != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / YEAR);
                        years += temp;
                        inVar -= (YEAR * temp);
                        sb.Append($" {years} years,");
                    }//years

                    else if (inVar >= WEEK && ((TFSUnit != Unit.Weeks) || (originalSeconds % WEEK != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / WEEK);
                        weeks += temp;
                        inVar -= (WEEK * temp);
                        sb.Append($" {weeks} weeks,");
                    }//weeks

                    else if (inVar >= DAY && ((TFSUnit != Unit.Days) || (originalSeconds % DAY != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / DAY);
                        days += temp;
                        inVar -= (DAY * temp);
                        sb.Append($" {days} days,");
                    }//days

                    else if (inVar >= HOUR && ((TFSUnit != Unit.Hours) || (originalSeconds % HOUR != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / HOUR);
                        hours += temp;
                        inVar -= (HOUR * temp);
                        sb.Append($" {hours} hours,");
                    }//hours

                    else if (inVar >= MINUTE && ((TFSUnit != Unit.Minutes) || (originalSeconds % MINUTE != 0) || (inVar != originalSeconds))) //Ensure this is not the original unit (unless original input had a fractional part of this unit)
                    {
                        temp = (ulong)Math.Truncate(inVar / MINUTE);
                        minutes += temp;
                        inVar -= (MINUTE * temp);
                        sb.Append($" {minutes} minutes,");
                    }//minutes

                    else if (inVar >= 1)
                    {
                        seconds = (ulong)Math.Truncate(inVar);
                        inVar -= seconds;
                        sb.Append($" {seconds} seconds,");
                    }//seconds

                    else if (subSecondOutput == false)
                    {
                        if (TFSUnit == Unit.Seconds && originalSeconds < 1)
                        {
                            eList.Add(ErrorCode.BadArguments);
                            throw new ArgumentOutOfRangeException("Your input was less than 1 second, but subseconds was disabled. Your input was: " + Input);
                        }
                        break;
                    }//subSecondCheck
                    else if (inVar >= DECISECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / DECISECOND);
                        deciseconds += temp;
                        inVar -= (DECISECOND * temp);
                        sb.Append($" {deciseconds} deciseconds,");
                    }//deciseconds

                    else if (inVar >= CENTISECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / CENTISECOND);
                        centiseconds += temp;
                        inVar -= (CENTISECOND * temp);
                        sb.Append($" {centiseconds} centiseconds,");
                    }//centiseconds

                    else if (inVar >= MILLISECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / MILLISECOND);
                        milliseconds += temp;
                        inVar -= (MILLISECOND * temp);
                        sb.Append($" {milliseconds} milliseconds,");
                    }//milliseconds

                    else if (inVar >= MYRIOSECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / MYRIOSECOND);
                        myrioseconds += temp;
                        inVar -= (MYRIOSECOND * temp);
                        sb.Append($" {myrioseconds} myrioseconds,");
                    }//myrioseconds

                    else if (inVar >= MICROSECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / MICROSECOND);
                        microseconds += temp;
                        inVar -= (MICROSECOND * temp);
                        sb.Append($" {microseconds} microseconds,");
                    }//microseconds

                    else if (inVar >= NANOSECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / NANOSECOND);
                        nanoseconds += temp;
                        inVar -= (NANOSECOND * temp);
                        sb.Append($" {nanoseconds} nanoseconds,");
                    }//nanoseconds

                    else if (inVar >= PICOSECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / PICOSECOND);
                        picoseconds += temp;
                        inVar -= (PICOSECOND * temp);
                        sb.Append($" {picoseconds} picoseconds,");
                    }//picoseconds

                    else if (inVar >= FEMTOSECOND && subSecondOutput)
                    {
                        temp = (ulong)Math.Truncate(inVar / FEMTOSECOND);
                        femtoseconds += temp;
                        inVar -= (FEMTOSECOND * temp);
                        sb.Append($" {femtoseconds} femtoseconds,");
                    }//femtoseconds
                }//while inVar larger than smallest unit
                MyDebugger($"Debug: original = {original}\nTFSUnit = {TFSUnit}\noriginalSeconds = {originalSeconds}\ninVar = {inVar}\n" +
                    $"millennia = {millennia}\ncenturies = {centuries}\ndecades = {decades}\nyears = {years}\nweeks = {weeks}\ndays = {days}\n" +
                    $"hours = {hours}\nminutes = {minutes}\nseconds = {seconds}\n" +
                    $"deciseconds = {deciseconds}\ncentiseconds = {centiseconds}\nmilliseconds = {milliseconds}\n" +
                    $"myrioseconds = {myrioseconds}\nmicroseconds = {microseconds}\nnanoseconds = {nanoseconds}\npicoseconds = {picoseconds}\nfemtoseconds = {femtoseconds}");

                //returns a string using string builder to only include units with non-zero values
                sb.Remove(sb.Length - 1, 1); //Remove the trailing ','
                return sb.ToString(); 
            }//else
        }//FromSeconds()




        private static void MyDebugger(string m)
        {
            if (DebugMode)
            {
                Trace.WriteLine("\n\n---------------debug---------------\n");
                Trace.WriteLine(m);
                Trace.WriteLine("\n-------------end debug-------------\n\n");
            }//if
        }//Error(String)

        private static void Error(string m, Exception e)
        {
            MyDebugger($"{m}\nError:\n\nData: {e.Data}\nMessage: {e.Message}\nSource: {e.Source}");
            Trace.WriteLine("\nSomething went wrong!\n\nError: " + e.Message);
        }//Error(String, Exception)

        private static void Error(Exception e)
        {
            MyDebugger($"Error:\n\nData: {e.Data}\nMessage: {e.Message}\nSource: {e.Source}");
            Trace.WriteLine("\nSomething went wrong!\n\nError: " + e.Message);
        }//Error(Exception)

        private static void Exit()
        {
            if (eList.Count == 0)
            {
                Environment.Exit((int)ErrorCode.Success);
            }
            else
            {
                ErrorCode[] eArray = eList.ToArray();
                int index = eArray.Length - 1;
                StringBuilder sb = new StringBuilder();
                foreach (ErrorCode e in eArray)
                    sb.Append($"\nError Code: {e}");
                Trace.WriteLine(sb.ToString());
                MyDebugger(sb.ToString());
                MyDebugger($"{(ErrorCode)eArray[index]}");
                Environment.Exit((int)eArray[index]);
            }
        }//ExitCode

        private static bool ArgsCheck(string testString, string argumentString)
        {
            if (String.IsNullOrWhiteSpace(testString))
            {
                eList.Add(ErrorCode.BadArguments);
                throw new ArgumentNullException("testString: {" + testString + "}");
            }
            else if (String.IsNullOrWhiteSpace(argumentString))
            {
                eList.Add(ErrorCode.BadArguments);
                throw new ArgumentNullException("argumentString: {" + argumentString + "}");
            }
            else
            {
                testString = testString.ToLowerInvariant();
                argumentString = argumentString.ToLowerInvariant();

                if (testString.Equals($"--{argumentString}") || testString.Equals($"-{argumentString[0]}"))
                {
                    return true;
                }//if
                else
                    return false;
            }//else
        }//ArgsCheck()
    }//TimeFromSeconds
}//namespace
