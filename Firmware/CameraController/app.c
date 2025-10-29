#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"


/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);


/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "CameraController";

void hwbp_app_initialize(void)
{
    /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 0;
    uint8_t fwH = 1;
    uint8_t fwL = 2;
    uint8_t ass = 0;
    
    /* Start core */
    core_func_start_core(
        1168,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name,
		false,	// The device is _not_ able to repeat the harp timestamp clock
		false,	// The device is _not_ able to generate the harp timestamp clock
		0		// Default timestamp offset
    );

}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	timer_type0_stop(&TCC0);	// Stop Cam0 or Motor0
	timer_type0_stop(&TCD0);	// Stop Cam1 or Motor1

	clr_CAM0_TRIG;
	clr_CAM1_TRIG;
	clr_CAM0_SYNC;
	clr_CAM1_SYNC;
}

/************************************************************************/
/* General definitions                                                  */
/************************************************************************/
// #define NBYTES 23

/************************************************************************/
/* General used functions                                               */
/************************************************************************/
/* Load external functions if needed */
//#include "hwbp_app_pwm_gen_funcs.c"

bool camera0_pulse;
int8_t camera0_sync_sec_counter;

#define rise_cam0_trig_now	set_CAM0_TRIG; \
                            if (app_regs.REG_EVT_EN & B_EVT_CAMS) \
                            {   \
                                app_regs.REG_CAM0 = 1;  \
                                core_func_send_event(ADD_REG_CAM0, true);   \
                            }

void start_camera0(void)
{
	uint16_t camera_freq = app_regs.REG_CAM0_FREQ << 1;	// Multiply CAM_FREQ x 2

	if (TCC0_CTRLA == 0 || TCC0_CTRLB != 0)					// Timer not running or in PWM mode
	{
		camera0_pulse = false;
		camera0_sync_sec_counter = -1;

		if (read_CAM0_SYNC && (app_regs.REG_EVT_EN & B_EVT_CAMS))
		{
			app_regs.REG_SYNC0 = 0;
			core_func_send_event(ADD_REG_SYNC0, true);
		}		
		clr_CAM0_SYNC;

		if (camera_freq < 8)
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV256, (32000000/256)/camera_freq, INT_LEVEL_LOW);
            rise_cam0_trig_now;
		}
		else if (camera_freq < 64)
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV64, (32000000/64)/camera_freq, INT_LEVEL_LOW);
			rise_cam0_trig_now;
		}
		else if (camera_freq < 128)
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV8, (32000000/8)/camera_freq, INT_LEVEL_LOW);
			rise_cam0_trig_now;
		}
		else if (camera_freq < 256)
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV4, (32000000/4)/camera_freq, INT_LEVEL_LOW);
			rise_cam0_trig_now;
		}
		else /*if (camera_freq <= 512)*/
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV2, (32000000/2)/camera_freq, INT_LEVEL_LOW);
			rise_cam0_trig_now;
		}
		/*
		else if (camera_freq <= 1024)
		{
			timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV1, (32000000/1)/camera_freq, INT_LEVEL_LOW);
		}
		*/
	}
}
bool camera1_pulse;
int8_t camera1_sync_sec_counter;

#define rise_cam1_trig_now	set_CAM1_TRIG; \
                            if (app_regs.REG_EVT_EN & B_EVT_CAMS) \
                            {   \
                                app_regs.REG_CAM1 = 1;  \
                                core_func_send_event(ADD_REG_CAM1, true);   \
                            }

void start_camera1(void)
{
	uint16_t camera_freq = app_regs.REG_CAM1_FREQ << 1;	// Multiply CAM_FREQ x 2

	if (TCD0_CTRLA == 0 || TCD0_CTRLB != 0)					// Timer not running or in PWM mode
	{
		camera1_pulse = false;
		camera1_sync_sec_counter = -1;

		if (read_CAM1_SYNC && (app_regs.REG_EVT_EN & B_EVT_CAMS))
		{
			app_regs.REG_SYNC1 = 0;
			core_func_send_event(ADD_REG_SYNC1, true);
		}		
		clr_CAM1_SYNC;

		if (camera_freq < 8)
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV256, (32000000/256)/camera_freq, INT_LEVEL_LOW);
			rise_cam1_trig_now;
		}
		else if (camera_freq < 64)
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV64, (32000000/64)/camera_freq, INT_LEVEL_LOW);
			rise_cam1_trig_now;
		}
		else if (camera_freq < 128)
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV8, (32000000/8)/camera_freq, INT_LEVEL_LOW);
			rise_cam1_trig_now;
		}
		else if (camera_freq < 256)
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV4, (32000000/4)/camera_freq, INT_LEVEL_LOW);
			rise_cam1_trig_now;
		}
		else /*if (camera_freq <= 512)*/
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV2, (32000000/2)/camera_freq, INT_LEVEL_LOW);
			rise_cam1_trig_now;
		}
		/*
		else if (camera_freq <= 1024)
		{
			timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV1, (32000000/1)/camera_freq, INT_LEVEL_LOW);
		}
		*/
	}
}

bool stop_cam0 = false;
bool stop_cam1 = false;

void stop_camera0(void)
{
	if (TCC0_CTRLA != 0 && TCC0_CTRLB == 0)	// Running but not running PWM mode
	{
		stop_cam0 = true;
	}
}
void stop_camera1(void)
{
	if (TCD0_CTRLA != 0 && TCD0_CTRLB == 0)	// Running but not running PWM mode
	{
    	stop_cam1 = true;
	}
}

void enable_motor0(void)
{
	if (TCC0_CTRLA == 0 || TCC0_CTRLB == 0)	// Not running or not in PWM mode
	{
		timer_type0_pwm(&TCC0, TIMER_PRESCALER_DIV64, (app_regs.REG_CAM0_MMODE_PERIOD >> 1), (app_regs.REG_CAM0_MMODE_PULSE >> 1), INT_LEVEL_OFF, INT_LEVEL_OFF);
		clr_CAM0_TRIG;
	}
}
void enable_motor1(void)
{
	if (TCD0_CTRLA == 0 || TCD0_CTRLB == 0)	// Not running or not in PWM mode
	{
		timer_type0_pwm(&TCD0, TIMER_PRESCALER_DIV64, (app_regs.REG_CAM1_MMODE_PERIOD >> 1), (app_regs.REG_CAM1_MMODE_PULSE >> 1), INT_LEVEL_OFF, INT_LEVEL_OFF);
		clr_CAM1_TRIG;
	}
}
void disable_motor0(void)
{
	if (TCC0_CTRLA != 0 && TCC0_CTRLB != 0)	// Running PWM mode
	{
		timer_type0_stop(&TCC0);
	}
}
void disable_motor1(void)
{
	if (TCD0_CTRLA != 0 && TCD0_CTRLB != 0)	// Running PWM mode
	{
		timer_type0_stop(&TCD0);
	}
}


/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_define_clock_default(void) {}
	
void core_callback_initialize_hardware(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	app_regs.REG_START_CAMS = 0;
	app_regs.REG_STOP_CAMS = 0;
	app_regs.REG_ENABLE_MOTORS = 0;
	app_regs.REG_DISABLE_MOTORS = 0;
	app_regs.REG_SET_OUTPUTS = 0;
	app_regs.REG_CLR_OUTPUTS = 0;
	app_regs.REG_OUTPUTS = 0;
	
	app_regs.REG_RESERVED0 = 0;

	app_regs.REG_SYNC_INTERVAL = 1;
	app_regs.REG_RESERVED1 = 0;

	app_regs.REG_IN0_MODE = GM_IN0_H_BOTH;
	
	app_regs.REG_CAM0_MODE = GM_CAM0_MODE_CAM;
	app_regs.REG_CAM0_FREQ = 30;
	app_regs.REG_CAM0_MMODE_PERIOD = 20000;
	app_regs.REG_CAM0_MMODE_PULSE = 1500;
	app_regs.REG_CAM1_MODE = GM_CAM1_MODE_CAM;
	app_regs.REG_CAM1_FREQ = 30;
	app_regs.REG_CAM1_MMODE_PERIOD = 20000;
	app_regs.REG_CAM1_MMODE_PULSE = 1500;
	
	app_regs.REG_RESERVED2 = 0;
	app_regs.REG_RESERVED3 = 0;
	
	app_regs.REG_EVT_EN = B_EVT_CAMS | B_EVT_IN0;
}

void core_callback_registers_were_reinitialized(void)
{
	/* Reset start bits */
	app_regs.REG_START_CAMS = 0;
	app_regs.REG_STOP_CAMS = 0;
	app_regs.REG_ENABLE_MOTORS = 0;
	app_regs.REG_DISABLE_MOTORS = 0;
	app_regs.REG_SET_OUTPUTS = 0;
	app_regs.REG_CLR_OUTPUTS = 0;
	
	/* Update outputs */
	app_write_REG_OUTPUTS(&app_regs.REG_OUTPUTS);
    
    /* Update modes */
    app_write_REG_IN0_MODE(&app_regs.REG_IN0_MODE);
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void) {}
void core_callback_visualen_to_off(void) {}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void)
{
	stop_camera0();
	stop_camera1();
	disable_motor0();
	disable_motor1();
}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
uint16_t _2000ms_counter = 0;

void core_callback_t_before_exec(void)
{
	if (++_2000ms_counter == 2000)
	{
		if ((TCC0_CTRLA != 0) && (TCC0_CTRLB == 0))
		{
			if (++camera0_sync_sec_counter == app_regs.REG_SYNC_INTERVAL)
			{
				camera0_sync_sec_counter = 0;

				tgl_CAM0_SYNC;
				if (app_regs.REG_EVT_EN & B_EVT_CAMS)
				{
					app_regs.REG_SYNC0 = read_CAM0_SYNC ? B_SYNC0 : 0;
					core_func_send_event(ADD_REG_SYNC0, true);
				}
			}
		}

		if ((TCD0_CTRLA != 0) && (TCD0_CTRLB == 0))
		{
			if (++camera1_sync_sec_counter == app_regs.REG_SYNC_INTERVAL)
			{
				camera1_sync_sec_counter = 0;

				tgl_CAM1_SYNC;
				if (app_regs.REG_EVT_EN & B_EVT_CAMS)
				{
					app_regs.REG_SYNC1 = read_CAM1_SYNC ? B_SYNC1 : 0;
					core_func_send_event(ADD_REG_SYNC1, true);
				}
			}
		}
	}
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) { _2000ms_counter = 0; }
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void) {}

/************************************************************************/
/* Callbacks: clock control                                              */
/************************************************************************/
void core_callback_clock_to_repeater(void) {}
void core_callback_clock_to_generator(void) {}
void core_callback_clock_to_unlock(void) {}
void core_callback_clock_to_lock(void) {}
	
/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}