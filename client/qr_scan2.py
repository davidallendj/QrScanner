import cv2
from pyzbar import pyzbar


def read_barcodes(frame):
	barcodes = pyzbar.decode(frame)
	found = False
	for barcode in barcodes:
		x, y , w, h = barcode.rect
		

		barcode_info = barcode.data.decode('utf-8')
		cv2.rectangle(frame, (x, y),(x+w, y+h), (0, 255, 0), 2)
		
		font = cv2.FONT_HERSHEY_DUPLEX
		cv2.putText(frame, barcode_info, (x + 6, y - 6), font, 0.5, (255, 255, 255), 1)
		print(barcode_info, end="")
		found = len(barcode_info) != 0
		
	return frame, found


def main() -> None:
	# Set video capture
	camera = cv2.VideoCapture(0)
	ret, frame = camera.read()

	# Run decoding until 'Esc' pressed
	while ret:
		ret, frame = camera.read()
		frame, found = read_barcodes(frame)
		cv2.imshow("", frame)
		if found:
			break
		if cv2.waitKey(1) & 0xFF == 27:
			break

	# Release camera and close app	
	camera.release()
	cv2.destroyAllWindows()


if __name__ == "__main__":
	main()