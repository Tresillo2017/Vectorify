import matplotlib.pyplot as plt
import numpy as np

# Creamos los datos de la recta
x = np.linspace(-5, 5, 100)
y = 2*x + 3

# Creamos los datos del vector
v = np.array([1, 2])
p = np.array([0, 0])

# Creamos el subplot y dibujamos la recta
fig, ax = plt.subplots()
ax.plot(x, y, label='Recta')

# Dibujamos el vector
ax.annotate('', xy=v, xytext=p, arrowprops=dict(facecolor='red', shrink=0.05))

# Configuramos los límites del gráfico
ax.set_xlim([-5, 5])
ax.set_ylim([-10, 10])

# Añadimos la leyenda
ax.legend()

# Mostramos el gráfico
plt.show()
