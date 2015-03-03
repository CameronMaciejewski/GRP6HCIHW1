require_relative('./read.rb')
record = ARGV.empty? ? "000000" : ARGV.first
file_str = File.join(File.dirname(File.expand_path(__FILE__)), "..","data","record_#{record}.dat")
file = File.new(file_str)
read_record file
