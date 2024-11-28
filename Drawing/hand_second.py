from PIL import Image, ImageDraw

# create 4096x4096 image transparent image
hour = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
# draw a trapazoid top 147 bottom 212 height 1884 shifted down by 491
dhour = ImageDraw.Draw(hour)
top = 57
bottom = 57
height = 1277 + 675
shift = 675

dhour.polygon([(2048-top//2, 2048-height+ shift), (2048-bottom//2, 2048+ shift), (2048+bottom//2, 2048+ shift), (2048+top//2, 2048-height + shift)], fill=(255, 0, 0, 255))

# draw red disk of radius 430 centered at (2048, 771)
dhour.ellipse((2048-430//2, 771-430//2, 2048+430//2, 771+430//2), fill=(255, 0, 0, 255))

hour.save('hand_second.png')