
#Creates an AP
systemctl start hostapd
systemctl start dnsmasq
ip addr add 192.168.4.1/24 dev wlp1s0

#Allows routing
sysctl -w net.ipv4.ip_forward=1
iptables -A FORWARD -i wlp1s0 -o enxae88fd6293ba -j ACCEPT
sudo iptables -A FORWARD -i enxae88fd6293ba -o wlp1s0 -m conntrack --ctstate RELATED,ESTABLISHED -j ACCEPT
iptables -t nat -A POSTROUTING -o enxae88fd6293ba -j MASQUERADE

