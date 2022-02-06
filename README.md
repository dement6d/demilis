```
               :::::::-.  .,::::::  .        :   ::: :::     ::: .::::::.                
                ;;,   `';,;;;;''''  ;;,.    ;;;  ;;; ;;;     ;;;;;;`    `                
                `[[     [[ [[cccc   [[[[, ,[[[[, [[[ [[[     [[['[==/[[[[,               
                 $$,    $$ $$""""   $$$$$$$$"$$$ $$$ $$'     $$$  '''    $               
                 888_,o8P' 888oo,__ 888 Y88" 888o888o88oo,.__888 88b    dP               
                 MMMMP"`   """"YUMMMMMM  M'  "MMMMMM""""YUMMMMMM  "YMmMY"                
                           The Multiple TCP Connection Listener                           

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
```
## Installation (Windows)
1. Download the repository [here](https://github.com/dement6d/demilis/archive/refs/heads/main.zip) OR clone it with `git clone https://github.com/dement6d/demilis.git`
2. If you downloaded the repository from [here](https://github.com/dement6d/demilis/archive/refs/heads/main.zip), extract it to a folder using [7zip](https://sourceforge.net/projects/sevenzip/files/7-Zip/) or another preferred method
3. Run the `install_win.bat` file
4. The executable will be located in `bin/Release/net6.0/win-x64/publish/`

## Installation (Linux)
1. Download the repository [here](https://github.com/dement6d/demilis/archive/refs/heads/main.zip) OR clone it with `git clone https://github.com/dement6d/demilis.git`
2. If you downloaded the repository from [here](https://github.com/dement6d/demilis/archive/refs/heads/main.zip), extract it to a folder using `unzip demilis-main.zip`
3. Run `cd demilis/` or `cd demilis-main/` depending on what the folder containing the repository is called
4. Run `sudo ./install_linux.sh` to install demilis
5. Run `demilis` in your terminal to start demilis

## Usage
Running `demilis` will print the help page with all arugments it can be ran with
For listening on an IP and port the required arguments are `-h` (`--host`) and `-p` (`--port`)

Once you start listening on an IP and port you will be inside the demilis command line, from here you can type `help` and see all available commands (different from arguments when launching demilis)

After you've recieved a session, you can interact with it by running the `session` command seperated with a space and followed by a session number. `Example: 'session 1'`
To see which session numbers are available to you, you can run the `list` command
Optionally, you can give a session a nickname by running the `nick` command and supplying it with a session number, followed by the desired nickname (excluding spaces). `Example: 'nick 1 Client1'` 

To exit demilis, run the `exit` command or press `Ctrl+C`
