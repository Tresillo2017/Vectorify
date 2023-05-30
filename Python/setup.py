from cx_Freeze import setup, Executable

base = None    

executables = [Executable("vector.py", base=base)]

packages = ["idna", "tkinter", "matplotlib", "numpy", "tkinter"]
options = {
    'build_exe': {    
        'packages':packages,
    },    
}

setup(
    name = "Vectorify",
    options = options,
    version = "2",
    description = 'Vector painter',
    executables = executables
)