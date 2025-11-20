systemctl start hostapd
systemctl start dnsmasq
sudo ip addr add 192.168.4.1/24 dev wlp1s0
cd /home/chris/Sharing
python3 -m http.server
 
