#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#define F_CPU 32000000
#include <util/delay.h>


/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_START_CAMS,
	&app_read_REG_STOP_CAMS,
	&app_read_REG_ENABLE_MOTORS,
	&app_read_REG_DISABLE_MOTORS,
	&app_read_REG_SET_OUTPUTS,
	&app_read_REG_CLR_OUTPUTS,
	&app_read_REG_OUTPUTS,
	&app_read_REG_INPUT0,
	&app_read_REG_CAM0,
	&app_read_REG_CAM1,
	&app_read_REG_SYNC0,
	&app_read_REG_SYNC1,
	&app_read_REG_MOTORS_STATE,
	&app_read_REG_RESERVED0,
	&app_read_REG_SYNC_INTERVAL,
	&app_read_REG_RESERVED1,
	&app_read_REG_IN0_MODE,
	&app_read_REG_CAM0_MODE,
	&app_read_REG_CAM0_FREQ,
	&app_read_REG_CAM0_MMODE_PERIOD,
	&app_read_REG_CAM0_MMODE_PULSE,
	&app_read_REG_CAM1_MODE,
	&app_read_REG_CAM1_FREQ,
	&app_read_REG_CAM1_MMODE_PERIOD,
	&app_read_REG_CAM1_MMODE_PULSE,
	&app_read_REG_RESERVED2,
	&app_read_REG_RESERVED3,
	&app_read_REG_EVT_EN
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_START_CAMS,
	&app_write_REG_STOP_CAMS,
	&app_write_REG_ENABLE_MOTORS,
	&app_write_REG_DISABLE_MOTORS,
	&app_write_REG_SET_OUTPUTS,
	&app_write_REG_CLR_OUTPUTS,
	&app_write_REG_OUTPUTS,
	&app_write_REG_INPUT0,
	&app_write_REG_CAM0,
	&app_write_REG_CAM1,
	&app_write_REG_SYNC0,
	&app_write_REG_SYNC1,
	&app_write_REG_MOTORS_STATE,
	&app_write_REG_RESERVED0,
	&app_write_REG_SYNC_INTERVAL,
	&app_write_REG_RESERVED1,
	&app_write_REG_IN0_MODE,
	&app_write_REG_CAM0_MODE,
	&app_write_REG_CAM0_FREQ,
	&app_write_REG_CAM0_MMODE_PERIOD,
	&app_write_REG_CAM0_MMODE_PULSE,
	&app_write_REG_CAM1_MODE,
	&app_write_REG_CAM1_FREQ,
	&app_write_REG_CAM1_MMODE_PERIOD,
	&app_write_REG_CAM1_MMODE_PULSE,
	&app_write_REG_RESERVED2,
	&app_write_REG_RESERVED3,
	&app_write_REG_EVT_EN
};

extern void start_camera0(void);
extern void start_camera1(void);
extern void stop_camera0(void);
extern void stop_camera1(void);
extern void enable_motor0(void);
extern void enable_motor1(void);
extern void disable_motor0(void);
extern void disable_motor1(void);

/************************************************************************/
/* REG_START_CAMS                                                       */
/************************************************************************/
void app_read_REG_START_CAMS(void) { app_regs.REG_START_CAMS = 0; }
bool app_write_REG_START_CAMS(void *a)
{
	if (*((uint8_t*)a) & B_START_CAM0)
	{
        clr_CAM0_TRIG;
        _delay_us(16);      // Measured, gives around 55us before the the first trigger pulse
                
		start_camera0();
		app_regs.REG_CAM0_MODE = GM_CAM0_MODE_CAM;
	}

	if (*((uint8_t*)a) & B_START_CAM1)
	{
        clr_CAM1_TRIG;
        _delay_us(16);      // Measured, gives around 55us before the the first trigger pulse

		start_camera1();
		app_regs.REG_CAM1_MODE = GM_CAM1_MODE_CAM;
	}

	return true;
}


/************************************************************************/
/* REG_STOP_CAMS                                                        */
/************************************************************************/
void app_read_REG_STOP_CAMS(void) { app_regs.REG_STOP_CAMS = 0; }
bool app_write_REG_STOP_CAMS(void *a)
{
	if (*((uint8_t*)a) & B_STOP_CAM0)
	{
		stop_camera0();
	}

	if (*((uint8_t*)a) & B_STOP_CAM1)
	{
		stop_camera1();
	}

	return true;
}


/************************************************************************/
/* REG_ENABLE_MOTORS                                                    */
/************************************************************************/
void app_read_REG_ENABLE_MOTORS(void) { app_regs.REG_ENABLE_MOTORS = 0; }
bool app_write_REG_ENABLE_MOTORS(void *a)
{
	if (*((uint8_t*)a) & B_EN_MOTOR0)
	{
		enable_motor0();
		app_regs.REG_CAM0_MODE = GM_CAM0_MODE_MOTOR;
	}

	if (*((uint8_t*)a) & B_EN_MOTOR1)
	{
		enable_motor1();
		app_regs.REG_CAM1_MODE = GM_CAM1_MODE_MOTOR;
	}

	return true;
}


/************************************************************************/
/* REG_DISABLE_MOTORS                                                   */
/************************************************************************/
void app_read_REG_DISABLE_MOTORS(void) { app_regs.REG_DISABLE_MOTORS = 0; }
bool app_write_REG_DISABLE_MOTORS(void *a)
{
	if (*((uint8_t*)a) & B_DIS_MOTOR0)
	{
		disable_motor0();
	}

	if (*((uint8_t*)a) & B_DIS_MOTOR1)
	{
		disable_motor1();
	}

	return true;
}


/************************************************************************/
/* REG_SET_OUTPUTS                                                      */
/************************************************************************/
void app_read_REG_SET_OUTPUTS(void) { app_regs.REG_SET_OUTPUTS = 0; }
bool app_write_REG_SET_OUTPUTS(void *a)
{
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG0) set_CAM0_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC0) set_CAM0_SYNC;
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG1) set_CAM1_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC1) set_CAM1_SYNC;
    
    /* Update REG_OUTPUTs since this value will be used on the function */
    /* core_callback_registers_were_reinitialized() to set the default  */
    /* values of the outputs.                                           */
    app_regs.REG_OUTPUTS = read_CAM0_TRIG ? B_OUT_CAM_TRIG0 : 0;
    app_regs.REG_OUTPUTS |= read_CAM0_SYNC ? B_OUT_CAM_SYNC0 : 0;
    app_regs.REG_OUTPUTS |= read_CAM1_TRIG ? B_OUT_CAM_TRIG1 : 0;
    app_regs.REG_OUTPUTS |= read_CAM1_SYNC ? B_OUT_CAM_SYNC1 : 0;
        
	return true;
}


/************************************************************************/
/* REG_CLR_OUTPUTS                                                      */
/************************************************************************/
void app_read_REG_CLR_OUTPUTS(void) { app_regs.REG_CLR_OUTPUTS = 0; }
bool app_write_REG_CLR_OUTPUTS(void *a)
{
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG0) clr_CAM0_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC0) clr_CAM0_SYNC;
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG1) clr_CAM1_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC1) clr_CAM1_SYNC;
    
    /* Update REG_OUTPUTs since this value will be used on the function */
    /* core_callback_registers_were_reinitialized() to set the default  */
    /* values of the outputs.                                           */
    app_regs.REG_OUTPUTS = read_CAM0_TRIG ? B_OUT_CAM_TRIG0 : 0;
    app_regs.REG_OUTPUTS |= read_CAM0_SYNC ? B_OUT_CAM_SYNC0 : 0;
    app_regs.REG_OUTPUTS |= read_CAM1_TRIG ? B_OUT_CAM_TRIG1 : 0;
    app_regs.REG_OUTPUTS |= read_CAM1_SYNC ? B_OUT_CAM_SYNC1 : 0;
        
	return true;
}


/************************************************************************/
/* REG_OUTPUTS                                                          */
/************************************************************************/
void app_read_REG_OUTPUTS(void)
{
	app_regs.REG_OUTPUTS = read_CAM0_TRIG ? B_OUT_CAM_TRIG0 : 0;
	app_regs.REG_OUTPUTS |= read_CAM0_SYNC ? B_OUT_CAM_SYNC0 : 0;
	app_regs.REG_OUTPUTS |= read_CAM1_TRIG ? B_OUT_CAM_TRIG1 : 0;
	app_regs.REG_OUTPUTS |= read_CAM1_SYNC ? B_OUT_CAM_SYNC1 : 0;
}

bool app_write_REG_OUTPUTS(void *a)
{
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG0) set_CAM0_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC0) set_CAM0_SYNC;
	if (*((uint8_t*)a) & B_OUT_CAM_TRIG1) set_CAM1_TRIG;
	if (*((uint8_t*)a) & B_OUT_CAM_SYNC1) set_CAM1_SYNC;

	app_regs.REG_OUTPUTS = *(uint8_t*)a;
	return true;
}


/************************************************************************/
/* REG_INPUT0                                                           */
/************************************************************************/
void app_read_REG_INPUT0(void)
{
	app_regs.REG_INPUT0 = read_INPUT0 ? B_IN0 : 0;
}
bool app_write_REG_INPUT0(void *a) { return false; }


/************************************************************************/
/* REG_CAM0                                                             */
/************************************************************************/
void app_read_REG_CAM0(void)
{
	app_regs.REG_CAM0 = ((TCC0_CTRLA != 0) && (TCC0_CTRLB == 0)) ? B_CAM0 : 0;
}
bool app_write_REG_CAM0(void *a) { return false; }


/************************************************************************/
/* REG_CAM1                                                             */
/************************************************************************/
void app_read_REG_CAM1(void)
{
	app_regs.REG_CAM1 = ((TCD0_CTRLA != 0) && (TCD0_CTRLB == 0)) ? B_CAM1 : 0;
}
bool app_write_REG_CAM1(void *a) { return false; }


/************************************************************************/
/* REG_SYNC0                                                            */
/************************************************************************/
void app_read_REG_SYNC0(void)
{
	app_regs.REG_SYNC0 = read_CAM0_SYNC ? B_SYNC0 : 0;
}
bool app_write_REG_SYNC0(void *a) { return false; }


/************************************************************************/
/* REG_SYNC1                                                            */
/************************************************************************/
void app_read_REG_SYNC1(void)
{
	app_regs.REG_SYNC1 = read_CAM1_SYNC ? B_SYNC1 : 0;
}
bool app_write_REG_SYNC1(void *a) { return false; }


/************************************************************************/
/* REG_MOTORS_STATE                                                     */
/************************************************************************/
void app_read_REG_MOTORS_STATE(void)
{
	app_regs.REG_MOTORS_STATE = ((TCC0_CTRLA != 0) && (TCC0_CTRLB != 0)) ? B_MOTOR0 : 0;
	app_regs.REG_MOTORS_STATE |= ((TCD0_CTRLA != 0) && (TCD0_CTRLB != 0)) ? B_MOTOR1 : 0;
}
bool app_write_REG_MOTORS_STATE(void *a) { return false; }


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void) {}
bool app_write_REG_RESERVED0(void *a)
{
	app_regs.REG_RESERVED0 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_SYNC_INTERVAL                                                    */
/************************************************************************/
void app_read_REG_SYNC_INTERVAL(void) {}
bool app_write_REG_SYNC_INTERVAL(void *a)
{
	if (*((uint8_t*)a) < 1 || *((uint8_t*)a) > 100)
		return false;

	app_regs.REG_SYNC_INTERVAL = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void) {}
bool app_write_REG_RESERVED1(void *a)
{
	app_regs.REG_RESERVED1 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_IN0_MODE                                                         */
/************************************************************************/
void app_read_REG_IN0_MODE(void) {}
bool app_write_REG_IN0_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & ~MSK_IN0_MODE)
		return false;

	app_regs.REG_IN0_MODE = reg;
    
    if ((app_regs.REG_IN0_MODE == GM_IN0_H_CAM0) || (app_regs.REG_IN0_MODE == GM_IN0_L_CAM0) || (app_regs.REG_IN0_MODE == GM_IN0_NOT_USED))
    {
        if (app_regs.REG_CAM1_MODE == GM_CAM1_MODE_MOTOR)
        {
            enable_motor1();
        }
    }
    
    if ((app_regs.REG_IN0_MODE == GM_IN0_H_CAM1) || (app_regs.REG_IN0_MODE == GM_IN0_L_CAM1) || (app_regs.REG_IN0_MODE == GM_IN0_NOT_USED))
    {
        if (app_regs.REG_CAM0_MODE == GM_CAM0_MODE_MOTOR)
        {
            enable_motor0();
        }
    }
    
	return true;
}


/************************************************************************/
/* REG_CAM0_MODE                                                        */
/************************************************************************/
void app_read_REG_CAM0_MODE(void) {}
bool app_write_REG_CAM0_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg & ~MSK_CAM0_MODE)
		return false;

	app_regs.REG_CAM0_MODE = reg;
    
    if (app_regs.REG_CAM0_MODE == GM_CAM0_MODE_MOTOR)
    {
        if ((app_regs.REG_IN0_MODE == GM_IN0_H_CAM1) || (app_regs.REG_IN0_MODE == GM_IN0_L_CAM1) || (app_regs.REG_IN0_MODE == GM_IN0_NOT_USED))
        {
            enable_motor0();
        }
    }
    else
    {
        disable_motor0();
    }
    
	return true;
}


/************************************************************************/
/* REG_CAM0_FREQ                                                        */
/************************************************************************/
void app_read_REG_CAM0_FREQ(void) {}
bool app_write_REG_CAM0_FREQ(void *a)
{
	if (*((uint16_t*)a) < 2 || *((uint16_t*)a) > 1000)
		return false;
	
	app_regs.REG_CAM0_FREQ = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_CAM0_MMODE_PERIOD                                                */
/************************************************************************/
void app_read_REG_CAM0_MMODE_PERIOD(void) {}
bool app_write_REG_CAM0_MMODE_PERIOD(void *a)
{
	if (*((uint16_t*)a) < 1)
		return false;
	
	app_regs.REG_CAM0_MMODE_PERIOD = *((uint16_t*)a);
	if (TCC0_CTRLA != 0 && TCC0_CTRLB != 0)
	{
		TCC0_PER = (app_regs.REG_CAM0_MMODE_PERIOD >> 1) - 1;
	}

	return true;
}


/************************************************************************/
/* REG_CAM0_MMODE_PULSE                                                 */
/************************************************************************/
void app_read_REG_CAM0_MMODE_PULSE(void) {}
bool app_write_REG_CAM0_MMODE_PULSE(void *a)
{
	if (*((uint16_t*)a) < 1)
		return false;
	
	app_regs.REG_CAM0_MMODE_PULSE = *((uint16_t*)a);
	TCC0_CCA = (app_regs.REG_CAM0_MMODE_PULSE >> 1) - 1;

	return true;
}


/************************************************************************/
/* REG_CAM1_MODE                                                        */
/************************************************************************/
void app_read_REG_CAM1_MODE(void) {}
bool app_write_REG_CAM1_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg & ~MSK_CAM1_MODE)
		return false;

	app_regs.REG_CAM1_MODE = reg;
	
	if (app_regs.REG_CAM1_MODE == GM_CAM1_MODE_MOTOR)
	{
    	if ((app_regs.REG_IN0_MODE == GM_IN0_H_CAM0) || (app_regs.REG_IN0_MODE == GM_IN0_L_CAM0) || (app_regs.REG_IN0_MODE == GM_IN0_NOT_USED))
    	{
        	enable_motor1();
    	}
	}
    else
    {
        disable_motor1();
    }
	
	return true;
}


/************************************************************************/
/* REG_CAM1_FREQ                                                        */
/************************************************************************/
void app_read_REG_CAM1_FREQ(void) {}
bool app_write_REG_CAM1_FREQ(void *a)
{
	if (*((uint16_t*)a) < 2 || *((uint16_t*)a) > 1000)
		return false;
	
	app_regs.REG_CAM1_FREQ = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_CAM1_MMODE_PERIOD                                                */
/************************************************************************/
void app_read_REG_CAM1_MMODE_PERIOD(void) {}
bool app_write_REG_CAM1_MMODE_PERIOD(void *a)
{	
	if (*((uint16_t*)a) < 1)
		return false;

	app_regs.REG_CAM1_MMODE_PERIOD = *((uint16_t*)a);
	if (TCD0_CTRLA != 0 && TCD0_CTRLB != 0)
	{
		TCD0_PER = (app_regs.REG_CAM1_MMODE_PERIOD >> 1) - 1;
	}

	return true;
}


/************************************************************************/
/* REG_CAM1_MMODE_PULSE                                                 */
/************************************************************************/
void app_read_REG_CAM1_MMODE_PULSE(void) {}
bool app_write_REG_CAM1_MMODE_PULSE(void *a)
{
	if (*((uint16_t*)a) < 1)
		return false;
	
	app_regs.REG_CAM1_MMODE_PULSE = *((uint16_t*)a);
	TCD0_CCA = (app_regs.REG_CAM1_MMODE_PULSE >> 1) - 1;

	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void) {}
bool app_write_REG_RESERVED2(void *a)
{
	app_regs.REG_RESERVED2 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void) {}
bool app_write_REG_RESERVED3(void *a)
{
	app_regs.REG_RESERVED3 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_EVT_EN                                                           */
/************************************************************************/
void app_read_REG_EVT_EN(void) {}
bool app_write_REG_EVT_EN(void *a)
{
	app_regs.REG_EVT_EN = *((uint8_t*)a);
	return true;
}