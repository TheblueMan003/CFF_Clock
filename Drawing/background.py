from PIL import Image, ImageDraw

# create 4096x4096 image transparent image
background = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
# draw disc with radius 2048
# center of disc is at (2048, 2048)
# color of disc is (255, 255, 255, 255) 

dbackground = ImageDraw.Draw(background)
dbackground.ellipse((0, 0, 4096, 4096), fill=(0, 0, 0, 255))
dbackground.ellipse((6, 6, 4090, 4090), fill=(255, 255, 255, 255))

inner_circle = 100

# draw a rectangle of 53x143
minute = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
dminute = ImageDraw.Draw(minute)
dminute.rectangle((2048-57//2, inner_circle, 2048-53//2+57, inner_circle + 143), fill=(0, 0, 0, 255))

for i in range(60):
    dd = minute.rotate(6 * i)
    background.paste(dd, (0, 0), dd)

# draw a rectangle of 53x143
hour = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
dhour = ImageDraw.Draw(hour)
dhour.rectangle((2048-143//2, inner_circle, 2048-143//2+143, inner_circle + 491), fill=(0, 0, 0, 255))

for i in range(12):
    dd = hour.rotate(30 * i)
    background.paste(dd, (0, 0), dd)

background.save('background.png')